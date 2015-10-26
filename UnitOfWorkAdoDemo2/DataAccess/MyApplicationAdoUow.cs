using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public class MyApplicationAdoUow : AdoDbContext
    {
        public UserRepository Users { get; private set; }

        public MyApplicationAdoUow(IDbConnection connection) 
            : base(connection)
        {
            Users = new UserRepository(this);
        }
    }
}