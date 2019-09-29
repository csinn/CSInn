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
        void Delete(int key);
        IEnumerable<TModel> Get();
        TModel Get(int id);
        IEnumerable<TModel> Get(ISpecification<TModel, TVisitor> specification);
        TModel Find(ISpecification<TModel, TVisitor> specification);
    }
}
