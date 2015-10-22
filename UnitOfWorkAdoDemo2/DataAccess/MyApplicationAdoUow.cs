using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public class MyApplicationAdoUow : AdoUnitOfWork
    {
        public UserRepository Users { get; private set; }

        public MyApplicationAdoUow(IDbConnection connection, bool ownsConnection = true) 
            : base(connection, ownsConnection)
        {
            Users = new UserRepository(this);
        }
    }
}