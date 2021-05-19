using Core.Contracts;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public class AdminRepository : GenericRepository<AdminEntity>, IAdminRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public AdminRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<AdminEntity> GetAllAdmins()
        {
            return _tableBookingContext.Admins.AsEnumerable();
        }
    }
}