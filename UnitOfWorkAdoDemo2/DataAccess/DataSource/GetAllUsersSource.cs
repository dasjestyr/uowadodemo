using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class GetAllUsersSource : AdoQuerySource<object, User>
    {
        public GetAllUsersSource(IAdoUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            CommandText = "SELECT * FROM Users";
        }

        protected override User GetFromReader(IDataRecord rdr)
        {
            var user = new User();
            
            user.FirstName = rdr.GetString(0);
            user.LastName = rdr.GetString(1);
            user.Id = rdr.GetInt32(2);
            return user;
        }
    }
}