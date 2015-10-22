using System.Data;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public interface IAdoUnitOfWork : IUnitOfWork
    {
        IDbCommand CreateCommand();
    }
}