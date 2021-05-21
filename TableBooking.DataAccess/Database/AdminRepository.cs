using Core.Contracts.DataAccess;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        public AdminEntity GetUserWithUsername(string username)
        {
            return _tableBookingContext.Admins.FirstOrDefault(a => a.Username == username);
        }

        public bool ContainsUserWithUsername(string username)
        {
            return GetUserWithUsername(username) != null;
        }
    }
}