using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class DeleteUserSource : AdoNonQuerySource<User>
    {
        public DeleteUserSource(IAdoUnitOfWork unitOfWork, User user) 
            : base(unitOfWork)
        {
            CommandText = $"DELETE FROM Users WHERE FirstName = '{user.FirstName}' AND LastName = '{user.LastName}'";
        }
    }
}