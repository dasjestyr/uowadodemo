using System;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}