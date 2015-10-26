using System.Collections.Generic;
using System.Linq;
using UnitOfWorkAdoDemo2.DataAccess.DataSource;

namespace UnitOfWorkAdoDemo2.DataAccess.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly List<User> _users = new List<User>(); 
        private readonly IAdoUnitOfWork _unitOfWork;
        private readonly bool _enableCache;

        public UserRepository(IAdoUnitOfWork unitOfWork, bool enableCache = false)
        {
            _unitOfWork = unitOfWork;
            _enableCache = enableCache;
        }

        public IEnumerable<User> GetAll()
        {
            var source = new GetAllUsersSource(_unitOfWork);
            var users = source.Execute().ToList();

            if (_enableCache && users.Any())
            {
                _users.AddRange(users);
            }

            return users;
        }

        public User Find(int id)
        {
            var source = new FindUserSource(_unitOfWork, id);
            User user = null;

            // only use the cache if it was enabled
            if (_enableCache)
                user = _users.FirstOrDefault(u => u.Id == id);

            user = user ?? source.Execute().FirstOrDefault(u => u.Id == id);

            if(_enableCache && user != null)
                _users.Add(user);

            return user;
        }

        public void Add(User user)
        {
            var source = new AddUserSource(_unitOfWork, user);
            _unitOfWork.AddNonQuery(source);
        }

        public void Remove(User user)
        {
            var source = new DeleteUserSource(_unitOfWork, user);
            _unitOfWork.AddNonQuery(source);
        }
    }
}