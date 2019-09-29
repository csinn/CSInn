using System;
using System.Threading;
using System.Threading.Tasks;
using CSInn.Domain.Repositories.Repositories;
using CSInn.Domain.Repositories.UnitOfWork;
using CSInn.Infrastructure.Repositories.Context;
using CSInn.Infrastructure.Repositories.Repositories.CSInn.Experimental.EF.Repositories;

namespace CSInn.Infrastructure.Repositories.UoW
{
    public class CSInnUnitOfWork: ICSInnUnitOfWork
    {
        private readonly CSInnDbContext _context;

        private ILessonsRepository _lessons;

        public ILessonsRepository Lessons
        {
            get
            {
                if (_lessons == null)
                {
                    _lessons = new LessonsRepository(_context);
                }

                return _lessons;
            }
            private set => _lessons = value;
        }

        public CSInnUnitOfWork(CSInnDbContext context)
        {
            _context = context;
        }

        public void Save() => _context.SaveChanges();

        public Task SaveAsync(CancellationToken token = default) =>_context.SaveChangesAsync();

        #region IDisposable pattern
        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
