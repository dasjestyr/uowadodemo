using System.Collections.Generic;
using System.Linq;
using UnitOfWorkAdoDemo2.DataAccess.DataSource;

namespace UnitOfWorkAdoDemo2.DataAccess.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly IAdoUnitOfWork _unitOfWork;

        public UserRepository(IAdoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            var source = new GetAllUsersSource(_unitOfWork);
            var users = source.Execute(null);
            return users;
        }

        public User Find(int id)
        {
            var source = new FindUserSource(_unitOfWork, id);
            var users = source.Execute(null);
            return users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(User user)
        {
            var source = new AddUserSource(_unitOfWork, user);
            source.Execute(user);
        }

        public void Remove(User user)
        {
            var source = new DeleteUserSource(_unitOfWork, user);
            source.Execute(user);
        }
    }
}