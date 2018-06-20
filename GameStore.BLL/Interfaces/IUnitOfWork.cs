using System;

namespace GameStore.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}
