using System;
using System.Data;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }

        void Commit();
    }
}