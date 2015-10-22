using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class AddUserSource : AdoNonQuerySource<User>
    {
        public AddUserSource(IAdoUnitOfWork unitOfWork, User user)
            : base(unitOfWork)
        {
            CommandText = $"INSERT INTO Users(FirstName, LastName) VALUES('{user.FirstName}','{user.LastName}')";
        }
    }
}