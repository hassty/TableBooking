using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Contracts
{
    public interface IRepository
    {
        void SaveChanges();
    }

    public interface IRepository<Entity> : IRepository where Entity : class
    {
        void Add(Entity entity);

        void AddRange(IEnumerable<Entity> entities);

        Entity Get(int id);

        IEnumerable<Entity> GetAll();

        void Remove(Entity entity);

        void RemoveRange(IEnumerable<Entity> entities);
    }
}