using AutoMapper;
using Core.Contracts;
using Core.Dto.Users;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public class UserRepository : GenericRepository<UserEntity, UserDto>, IUserRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public UserRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public UserEntity GetUserWithUsername(string username)
        {
            var dbEntity = _tableBookingContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
            return _mapper.Map<UserEntity>(dbEntity);
        }

        public override void Remove(UserEntity entity)
        {
            var dbEntity = _tableBookingContext.Users.Where(r => r.Username.Equals(entity.Username)).FirstOrDefault();
            if (dbEntity == null)
            {
                return;
            }

            _tableBookingContext.Users.Remove(dbEntity);
        }

        public override void RemoveRange(IEnumerable<UserEntity> entities)
        {
            List<UserDto> dbEntities = new List<UserDto>();
            foreach (var entity in entities)
            {
                var user = _tableBookingContext.Users.Where(u => u.Username.Equals(entity.Username)).FirstOrDefault();
                if (user != null)
                {
                    dbEntities.Add(user);
                }
            }
            _tableBookingContext.Users.RemoveRange(dbEntities);
        }
    }
}