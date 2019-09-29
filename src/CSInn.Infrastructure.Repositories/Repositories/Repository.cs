using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSInn.Domain.Repositories.Repositories;
using CSInn.Domain.Repositories.Specifications.Base;
using CSInn.Infrastructure.Repositories.Context;
using CSInn.Infrastructure.Repositories.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Infrastructure.Repositories.Repositories
{
    public abstract class Repository<TModel, TDomainVisitor, TEFVisitor,  TEntity> : IRepository<TModel, TDomainVisitor>, IRepositoryAsync<TModel, TDomainVisitor>
        where TModel : class
        where TEntity : class
        where TDomainVisitor : ISpecificationVisitor<TDomainVisitor, TModel>
        where TEFVisitor : ExpressionVisitor<TEntity, TDomainVisitor, TModel>, new()
    {
        private readonly TEFVisitor _visitor;

        private readonly CSInnDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected Repository(CSInnDbContext context)
        {
            _context = context;
            _visitor = new TEFVisitor();
            _dbSet = _context.Set<TEntity>();
        }

        protected abstract TModel ToModel(TEntity entity);
        protected abstract TEntity ToEntity(TModel model);
        protected abstract int ExtractKey(TModel model);

        public IEnumerable<TModel> Get() => _dbSet.Select(e => ToModel(e));
        public void Create(TModel model) => _dbSet.Add(ToEntity(model));

        public void Update(TModel model)
        {
            var entity = _dbSet.Find(ExtractKey(model));

            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).CurrentValues.SetValues(model);
        }

        public void Delete(TModel model)
        {
            var entity = _dbSet.Find(ExtractKey(model));
            _dbSet.Remove(entity);
        }

        public TModel Find(ISpecification<TModel, TDomainVisitor> specification)
        {
            var expression = _visitor.ConvertSpecToExpression(specification);
            return ToModel(_dbSet.FirstOrDefault(expression));
        }

        public IEnumerable<TModel> Get(ISpecification<TModel, TDomainVisitor> specification)
        {
            var expression = _visitor.ConvertSpecToExpression(specification);
            return _dbSet.Where(expression).Select(e => ToModel(e));
        }

        public async Task CreateAsync(TModel model, CancellationToken token = default) => await _dbSet.AddAsync(ToEntity(model), token);

        public async Task UpdateAsync(TModel model)
        {
            var entity = await _dbSet.FindAsync(ExtractKey(model));

            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).CurrentValues.SetValues(model);
        }

        public async Task DeleteAsync(TModel model)
        {
            var entity = await _dbSet.FindAsync(ExtractKey(model));
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TModel>> GetAsync(CancellationToken token = default)
        {
            var entities = await _dbSet.ToListAsync(token);
            return entities.Select(ToModel);
        }

        public async Task<IEnumerable<TModel>> GetAsync(ISpecification<TModel, TDomainVisitor> specification, CancellationToken token = default)
        {
            var expression = _visitor.ConvertSpecToExpression(specification);
            var entities = await _dbSet.Where(expression).ToListAsync(token);

            return entities.Select(ToModel);
        }

        public async Task<TModel> FindAsync(ISpecification<TModel, TDomainVisitor> specification, CancellationToken token = default)
        {
            var expression = _visitor.ConvertSpecToExpression(specification);
            var entity = await _dbSet.FirstOrDefaultAsync(expression, token);

            return ToModel(entity);
        }

    }
}
