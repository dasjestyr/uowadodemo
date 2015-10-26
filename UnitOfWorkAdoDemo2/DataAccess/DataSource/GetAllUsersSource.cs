using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class GetAllUsersSource : AdoQuerySource<User>
    {
        public GetAllUsersSource(IAdoUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            
        }

        protected override User GetFromReader(IDataRecord rdr)
        {
            var user = new User
            {
                FirstName = rdr.GetString(0),
                LastName = rdr.GetString(1),
                Id = rdr.GetInt32(2)
            };

            return user;
        }

        protected override string GetCommandText()
        {
            return "SELECT * FROM Users";
        }
    }
}