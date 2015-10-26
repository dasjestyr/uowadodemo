using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class AddUserSource : AdoNonQuerySource
    {
        private readonly User _user;

        public AddUserSource(IAdoUnitOfWork unitOfWork, User user)
            : base(unitOfWork)
        {
            _user = user;
        }

        protected override string GetCommand()
        {
            return $"INSERT INTO Users(FirstName, LastName) VALUES('{_user.FirstName}','{_user.LastName}')";
        }
    }
}