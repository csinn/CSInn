using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSInn.Domain.Repositories.Repositories;
using CSInn.Domain.Repositories.Specifications.Base;
using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Infrastructure.Repositories.Context;
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
            private DbSet<LessonEntity> DbSet => _context.Lessons;

            public LessonsRepository(CSInnDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Lesson> Get() => DbSet.ToModels();
            public void Create(Lesson model) => DbSet.Add(model.ToEntity());

            public void Update(Lesson model)
            {
                var entity = DbSet.Find(model.Id);

                if (entity == null)
                {
                    return;
                }

                _context.Entry(entity).CurrentValues.SetValues(model);
            }

            public void Delete(Lesson model)
            {
                var entity = DbSet.Find(model.Id);
                DbSet.Remove(entity);
            }

            public Lesson Find(ISpecification<Lesson, ILessonSpecificationVisitor> specification)
            {
                var visitor = new LessonEntityExpressionVisitor();
                specification.Accept(visitor);
                var expression = visitor.Expression;

                return DbSet.FirstOrDefault(expression).ToModel();
            }

            public IEnumerable<Lesson> Get(ISpecification<Lesson, ILessonSpecificationVisitor> specification)
            {
                var visitor = new LessonEntityExpressionVisitor();
                var expression = visitor.ConvertSpecToExpression(specification);

                return DbSet.Where(expression).AsEnumerable().ToModels();
            }

            public async Task CreateAsync(Lesson model, CancellationToken token = default) => await DbSet.AddAsync(model.ToEntity(), token);

            public async Task UpdateAsync(Lesson model)
            {
                var entity = await DbSet.FindAsync(model.Id);
                
                if (entity == null)
                {
                    return;
                }

                _context.Entry(entity).CurrentValues.SetValues(model);
            }

            public async Task DeleteAsync(Lesson model)
            {
                var entity = await DbSet.FindAsync(model.Id);
                DbSet.Remove(entity);
            }

            public async Task<IEnumerable<Lesson>> GetAsync(CancellationToken token = default)
            {
                var entities = await DbSet.ToListAsync(token);
                return entities.ToModels();
            }

            public async Task<IEnumerable<Lesson>> GetAsync(ISpecification<Lesson, ILessonSpecificationVisitor> specification, CancellationToken token = default)
            {
                var visitor = new LessonEntityExpressionVisitor();
                var expression = visitor.ConvertSpecToExpression(specification);
                
                var entities = await DbSet.Where(expression).ToListAsync(token);

                return entities.ToModels();
            }

            public async Task<Lesson> FindAsync(ISpecification<Lesson, ILessonSpecificationVisitor> specification, CancellationToken token = default)
            {
                var visitor = new LessonEntityExpressionVisitor();
                var expression = visitor.ConvertSpecToExpression(specification);

                var entity = await DbSet.FirstOrDefaultAsync(expression, token);

                return entity.ToModel();
            }
        }
    }
}
