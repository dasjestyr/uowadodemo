using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.DataSource;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public interface IAdoUnitOfWork : IUnitOfWork
    {
        IDbCommand CreateCommand();

        void AddNonQuery(AdoNonQuerySource source);
    }
}