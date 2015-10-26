using UnitOfWorkAdoDemo2.DataAccess.Repository;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public class DeleteUserSource : AdoNonQuerySource
    {
        private readonly User _user;

        public DeleteUserSource(IAdoUnitOfWork unitOfWork, User user) 
            : base(unitOfWork)
        {
            _user = user;
        }

        protected override string GetCommand()
        {
            return $"DELETE FROM Users WHERE FirstName = '{_user.FirstName}' AND LastName = '{_user.LastName}'";
        }
    }
}