// -------------------------------------------------------------------------------------------------
// C# Inn Website - © Copyright 2020 - C# Inn
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace CSInn.Domain.Repositories
{
    public interface IRepository<TModel>
        where TModel : class
    {
        void Create(TModel model);

        void Update(TModel model);

        void Delete(int key);

        IEnumerable<TModel> Get();

        TModel Get(int id);
    }
}
