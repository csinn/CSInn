using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using CSInn.Models;
using CSInn.Infrastructure.Repositories.Entities;

namespace ConsoleApp1
{
    public static class Mapper
    {
        #region basic
        public static readonly Dictionary<string, Dictionary<string, string>> classMemberMapping = new Dictionary<string, Dictionary<string, string>>();
        private static IMapper mapper;

        static Mapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => {
                map<LessonEntity, Lesson>(cfg)
                    .bind(src => src.entity_test, dest => dest.model_test);
            });

            mapper = config.CreateMapper();
        }

        private class Mapping<TSource, TDestination>
        {
            public AutoMapper.IMappingExpression<TSource, TDestination> map1;
            public AutoMapper.IMappingExpression<TDestination, TSource> map2;

            private string getMemberName<T>(Expression<Func<T, object>> ex)
            {
                if (ex.Body is MemberExpression)
                    return (ex.Body as MemberExpression).Member.Name;
                else if (ex.Body is UnaryExpression)
                    return ((ex.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
                else
                    return null;
            }
            public Mapping<TSource, TDestination> bind(Expression<Func<TSource, object>> left, Expression<Func<TDestination, object>> right)
            {
                string leftMember = getMemberName(left);
                string rightMember = getMemberName(right);
                Dictionary<string, string> memberMapping = classMemberMapping[typeof(TSource).Name];
                memberMapping[leftMember] = rightMember;

                map2.ForMember(left, opt => opt.MapFrom(right));
                map1.ForMember(right, opt => opt.MapFrom(left));
                return this;
            }
        }

        private static Mapping<TSource, TDestination> map<TSource, TDestination>(AutoMapper.IMapperConfigurationExpression cfg)
        {
            classMemberMapping[typeof(TSource).Name] = new Dictionary<string, string>();

            return new Mapping<TSource, TDestination>()
            {
                map1 = cfg.CreateMap<TSource, TDestination>(),
                map2 = cfg.CreateMap<TDestination, TSource>()
            };
        }
        #endregion


        public static TModel mapEntityToModel<TModel, TEntity>(TEntity entity) where TModel : new()
        {
            TModel model = new TModel();
            mapper.Map(entity, model);
            return model;
        }

        public static List<TModel> mapListEntityToModel<TModel, TEntity>(List<TEntity> listEntities) where TModel : new()
        {
            List<TModel> listModel = new List<TModel>();
            foreach (TEntity entity in listEntities)
            {
                TModel model = mapEntityToModel<TModel, TEntity>(entity);
                listModel.Add(model);
            }
            return listModel;
        }

        public static TEntity mapModelToEntity<TModel, TEntity>(TModel model) where TEntity : new()
        {
            TEntity entity = new TEntity();
            mapper.Map(model, entity);
            return entity;
        }
    }

    public static class DaoLambdaExtension
    {
        public static string GetCacheKey<TSource, TDest>()
        {
            return string.Concat(typeof(TSource).FullName, typeof(TDest).FullName);
        }

        public static IEnumerable<MemberAssignment> createMemberBinding<TSource, TDest>(ParameterExpression parameterExpression)
        {
            string[] SourceExcludedMembers = { "Tags", "Authors" };
            string[] DestExcludedMembers = { "Tags", "Authors" };
            var sourceProperties = typeof(TSource).GetProperties().Where(src => !SourceExcludedMembers.Contains(src.Name));
            var destinationProperties = typeof(TDest).GetProperties().Where(dest => dest.CanWrite & !DestExcludedMembers.Contains(dest.Name));
            Dictionary<string, string> memberMapping = Mapper.classMemberMapping[typeof(TSource).Name];

            var bindings = destinationProperties
                                .Select(destinationProperty => BuildBinding<TSource, TDest>(memberMapping, parameterExpression, destinationProperty, sourceProperties))
                                .Where(d => d != null);

            return bindings;
        }

        private static bool propertiesEqual(Dictionary<string, string> memberMapping, string dest, string src)
        {
            string mapp;
            if (!memberMapping.TryGetValue(src, out mapp))
                return false;
            return dest == mapp;
        }

        private static MemberAssignment BuildBinding<TSource, TDest>(Dictionary<string, string> memberMapping,
            Expression parameterExpression, MemberInfo destinationProperty, IEnumerable<PropertyInfo> sourceProperties)
        {
            var sourceProperty = sourceProperties.FirstOrDefault(src => src.Name.ToLower() == destinationProperty.Name.ToLower()
                || (propertiesEqual(memberMapping, destinationProperty.Name, src.Name)));

            if (sourceProperty == null)
                return null;

            return Expression.Bind(destinationProperty, Expression.Property(parameterExpression, sourceProperty));
        }
    }


    public class ProjectionExpression<TSource>
    {
        private readonly IQueryable<TSource> _source;

        private static readonly Dictionary<string, Expression> ExpressionCache = new Dictionary<string, Expression>();

        public ProjectionExpression(IQueryable<TSource> source)
        {
            _source = source;
        }

        public IQueryable<TDest> To<TDest>()
        {
            var queryExpression = GetCachedExpression<TDest>() ?? BuildExpression<TDest>();

            return _source.Select(queryExpression);
        }

        private static Expression<Func<TSource, TDest>> GetCachedExpression<TDest>()
        {
            var key = DaoLambdaExtension.GetCacheKey<TSource, TDest>();

            return ExpressionCache.ContainsKey(key) ? ExpressionCache[key] as Expression<Func<TSource, TDest>> : null;
        }

        private static Expression<Func<TSource, TDest>> BuildExpression<TDest>()
        {
            var parameterExpression = Expression.Parameter(typeof(TSource), "src");
            var bindings = DaoLambdaExtension.createMemberBinding<TSource, TDest>(parameterExpression);

            var expression = Expression.Lambda<Func<TSource, TDest>>(Expression.MemberInit(Expression.New(typeof(TDest)), bindings), parameterExpression);

            var key = DaoLambdaExtension.GetCacheKey<TSource, TDest>();
            ExpressionCache.Add(key, expression);

            return expression;
        }
    }

    public static class ObjectSetExtensions
    {
        public static ProjectionExpression<TSource> Project<TSource>(
             this IQueryable<TSource> source)
        {
            return new ProjectionExpression<TSource>(source);
        }
    }

    public abstract class BasicDao<TEntity>
    {
        protected string entitySetName;
        public IQueryable<TEntity> EntitySet;
    }

    public class LessonDao : BasicDao<LessonEntity>
    {
        public LessonDao(IQueryable<LessonEntity> EntitySet)
        {
            this.EntitySet = EntitySet;
        }
    }


    public abstract class BasicDto<TModel, TEntity>
        where TEntity : new()
        where TModel : new()
    {
        protected BasicDao<TEntity> dao;

        public BasicDto(BasicDao<TEntity> dao) { this.dao = dao; }

        public IQueryable<TModel> get()
        {
            return dao.EntitySet.Project().To<TModel>();
        }
    }

    public class LessonDto : BasicDto<Lesson, LessonEntity>
    {
        public LessonDto(BasicDao<LessonEntity> dao)
             : base(dao)
        { }

        public List<Lesson> findByTest(string test, string desc)
        {
            var lessonModelList =
                /*
                (from p in get()
                 where !p.model_test.Equals(test)
                 select p)
                */
                get()
                .Where(p => !p.model_test.Equals(test))
                .Where(p => p.Description == desc)
                .ToList();


            return lessonModelList;
        }
    }
}
