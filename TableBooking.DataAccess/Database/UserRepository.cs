using Core.Contracts;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Database
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public UserEntity GetUserWithUsername(string username)
        {
            return _tableBookingContext.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }

    }


}