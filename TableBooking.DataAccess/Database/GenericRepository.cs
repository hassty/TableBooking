using Core.Contracts.DataAccess;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public abstract class GenericRepository<Entity> : IRepository<Entity> where Entity : class
    {
        protected DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        private bool ContainsEntity(Entity entity)
        {
            return _context.Set<Entity>().Contains(entity);
        }

        public void Add(Entity entity)
        {
            _context.Set<Entity>().Add(entity);
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            _context.Set<Entity>().AddRange(entities);
        }

        public Entity Get(int id)
        {
            return _context.Set<Entity>().Find(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return _context.Set<Entity>().ToList();
        }

        /// <exception cref="ItemNotFoundException"></exception>
        public void Remove(Entity entity)
        {
            if (_context.Set<Entity>().Contains(entity) == false)
            {
                throw new ItemNotFoundException("Item was not found or already deleted");
            }
            _context.Set<Entity>().Remove(entity);
        }

        public void RemoveAll()
        {
            _context.Set<Entity>().RemoveRange(_context.Set<Entity>());
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            var entitiesToDelete = entities.ToList();
            entitiesToDelete.RemoveAll(e => !ContainsEntity(e));

            _context.Set<Entity>().RemoveRange(entitiesToDelete);
        }

        public void Rollback()
        {
            var changedEntires = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntires)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Entity entity)
        {
            _context.Set<Entity>().Update(entity);
        }
    }
}