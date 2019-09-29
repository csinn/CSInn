using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSInn.Domain.Repositories.Specifications.Base;

namespace CSInn.Domain.Repositories.Repositories
{
    public interface IRepositoryAsync<TModel, out TVisitor>
        where TModel : class
        where TVisitor : ISpecificationVisitor<TVisitor, TModel>
    {
        Task CreateAsync(TModel model, CancellationToken token = default);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TModel key);
        Task<IEnumerable<TModel>> GetAsync(CancellationToken token = default);
        Task<IEnumerable<TModel>> GetAsync(ISpecification<TModel, TVisitor> specification, CancellationToken token = default);
        Task<TModel> FindAsync(ISpecification<TModel, TVisitor> specification, CancellationToken token = default);
    }
}