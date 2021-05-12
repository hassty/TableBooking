using AutoMapper;
using Core.Contracts;
using Core.Dto.Users;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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
    }
}