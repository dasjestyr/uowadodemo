using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class FindUserSource : AdoQuerySource<object, User>
    {
        public FindUserSource(IAdoUnitOfWork unitOfWork, int userId) 
            : base(unitOfWork)
        {
            CommandText = $"SELECT * FROM Users WHERE ID = {userId}";
        }

        protected override User GetFromReader(IDataRecord rdr)
        {
            return new User
            {
                Id = (int)rdr["ID"],
                FirstName = rdr["FirstName"] as string,
                LastName = rdr["LastName"] as string
            };
        }
    }
}
