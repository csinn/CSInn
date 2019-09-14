using System;
using System.Collections.Generic;
using System.Text;

namespace CSInn.Domain.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {
        void Create(TModel model);
        void Update(TModel model);
        void Delete(int key);
        IEnumerable<TModel> Get();
        TModel Get(int id);
    }
}
