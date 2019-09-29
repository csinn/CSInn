using System.Collections.Generic;
using CSInn.Domain.Repositories.Specifications.Base;

namespace CSInn.Domain.Repositories.Repositories
{
    public interface IRepository<TModel, out TVisitor> 
        where TModel : class 
        where TVisitor : ISpecificationVisitor<TVisitor, TModel>
    {
        void Create(TModel model);
        void Update(TModel model);
        IEnumerable<TModel> Get();
        IEnumerable<TModel> Get(ISpecification<TModel, TVisitor> specification);
        TModel Find(ISpecification<TModel, TVisitor> specification);
    }
}
