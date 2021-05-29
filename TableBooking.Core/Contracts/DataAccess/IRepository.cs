using System.Collections.Generic;

namespace Core.Contracts.DataAccess
{
    public interface IRepository
    {
        void Rollback();

        void SaveChanges();
    }

    public interface IRepository<Entity> : IRepository where Entity : class
    {
        void Add(Entity entity);

        void AddRange(IEnumerable<Entity> entities);

        Entity Get(int id);

        IEnumerable<Entity> GetAll();

        void Remove(Entity entity);

        void RemoveAll();

        void RemoveRange(IEnumerable<Entity> entities);

        void Update(Entity entity);
    }
}