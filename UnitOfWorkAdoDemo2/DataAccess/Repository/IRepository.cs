using System.Collections.Generic;

namespace UnitOfWorkAdoDemo2.DataAccess.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<User> GetAll();

        TEntity Find(int id);

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}