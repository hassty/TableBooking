using AutoMapper;
using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Database
{
    public class GenericRepository<Entity, DtoEntity> : IRepository<Entity> where Entity : class where DtoEntity : class
    {
        protected readonly IMapper _mapper;
        protected DbContext _context;

        public GenericRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(Entity entity)
        {
            var dbEntity = _mapper.Map<DtoEntity>(entity);
            _context.Set<DtoEntity>().Add(dbEntity);
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            var dbEntitiesList = entities.Select(e => _mapper.Map<DtoEntity>(e)).ToList();
            _context.Set<DtoEntity>().AddRange(dbEntitiesList);
        }

        public IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate)
        {
            //return _context.Set<DtoEntity>().Where(predicate);
            throw new NotImplementedException("GenericRepository Find");
        }

        public Entity Get(int id)
        {
            var entity = _context.Set<DtoEntity>().Find(id);
            return _mapper.Map<Entity>(entity);
        }

        public IEnumerable<Entity> GetAll()
        {
            var dbEntitiesList = _context.Set<DtoEntity>().ToList();
            return dbEntitiesList.Select(e => _mapper.Map<Entity>(e)).ToList();
        }

        public virtual void Remove(Entity entity)
        {
            var dbEntity = _mapper.Map<DtoEntity>(entity);
            _context.Set<DtoEntity>().Remove(dbEntity);
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            var dbEntitiesList = entities.Select(e => _mapper.Map<DtoEntity>(e)).ToList();
            _context.Set<DtoEntity>().RemoveRange(dbEntitiesList);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}