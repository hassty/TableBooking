using AutoMapper;
using Core.Contracts;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public class AdminRepository : UserRepository, IAdminRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public AdminRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<AdminEntity> GetAllAdmins()
        {
            throw new NotImplementedException("Get all admins");
        }
    }
}
