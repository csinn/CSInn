using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSInn.Experimental.EF.Data;
using CSInn.Experimental.EF.Repositories;

namespace CSInn.Experimental.EF
{
    public class UnitOfWork: IDisposable
    {
        public ExperimantalRepository Experiments { get; }
        public dynamic Lessons { get; }
        private readonly ExperimentContext _context;

        public UnitOfWork(ExperimentContext context)
        {
            _context = context;
            Experiments = new ExperimantalRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    public class Tesdttt
    {
        public void Test()
        {
            var uow = new UnitOfWork(new ExperimentContext(null));
            uow.Experiments.Create(null);
            uow.Experiments.Create(null);
            uow.Lessons.Create(null);
            uow.Save();

        }
    }
}
