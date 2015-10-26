using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class FindUserSource : AdoQuerySource<User>
    {
        private readonly int _userId;

        public FindUserSource(IAdoUnitOfWork unitOfWork, int userId) 
            : base(unitOfWork)
        {
            _userId = userId;
        }

        protected override User GetFromReader(IDataRecord rdr)
        {
            return new User
            {
                Id = rdr.GetInt32(2),
                FirstName = rdr.GetString(0),
                LastName = rdr.GetString(1)
            };
        }

        protected override string GetCommandText()
        {
            return $"SELECT * FROM Users WHERE ID = {_userId}";
        }
    }
}
