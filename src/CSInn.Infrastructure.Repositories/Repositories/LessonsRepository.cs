using System.Collections.Generic;
using System.Linq;
using CSInn.Domain.Repositories.Repositories;
using CSInn.Domain.Repositories.Specifications.Base;
using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Infrastructure.Repositories.Entities;
using CSInn.Infrastructure.Repositories.Expressions;
using CSInn.Infrastructure.Repositories.Extensions;
using CSInn.Models;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Infrastructure.Repositories.Repositories
{
    namespace CSInn.Experimental.EF.Repositories
    {
        public class LessonsRepository: ILessonsRepository
        {
            private readonly CSInnDbContext _context;
            private DbSet<LessonEntity> dbSet => _context.Lessons;

            public LessonsRepository(CSInnDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Lesson> Get() => dbSet.ToModels();
            public IEnumerable<Lesson> Get(string title) => dbSet.Where(e => e.Title == title).Select(e => e.ToModel());
            public void Create(Lesson model) => dbSet.Add(model.ToEntity());

            public void Update(Lesson model)
            {
                var entity = dbSet.Find(model.Id);

                if (entity == null)
                {
                    return;
                }

                _context.Entry(entity).CurrentValues.SetValues(model);
            }

            public void Delete(Lesson model)
            {
                var entity = dbSet.Find(model.Id);
                dbSet.Remove(entity);
            }

            public void Delete(int key)
            {
                throw new System.NotImplementedException();
            }

            public Lesson Get(int id)
            {
                throw new System.NotImplementedException();
            }

            public Lesson Find(ISpecification<Lesson, ILessonSpecificationVisitor> specification)
            {
                var visitor = new LessonEntityExpressionVisitor();
                specification.Accept(visitor);
                var expression = visitor.Expression;

                return dbSet.FirstOrDefault(expression).ToModel();
            }

            public IEnumerable<Lesson> Get(ISpecification<Lesson, ILessonSpecificationVisitor> specification)
            {
                var visitor = new LessonEntityExpressionVisitor();
                specification.Accept(visitor);
                var expression = visitor.Expression;

                var smth = expression.Compile();
                return dbSet.Where(smth).AsEnumerable().ToModels();
            }
        }
    }
}
