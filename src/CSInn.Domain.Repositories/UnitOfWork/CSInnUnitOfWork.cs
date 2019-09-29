using System;
using System.Threading;
using System.Threading.Tasks;
using CSInn.Domain.Repositories.Repositories;

namespace CSInn.Domain.Repositories.UnitOfWork
{
    public interface ICSInnUnitOfWork: IDisposable
    {
        ILessonsRepository Lessons { get; }

        void Save();
        Task SaveAsync(CancellationToken token = default);
    }
}
