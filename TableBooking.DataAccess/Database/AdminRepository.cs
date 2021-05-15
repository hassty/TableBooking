using AutoMapper;
using Core.Contracts;
using Core.Dto.Users;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public class AdminRepository : GenericRepository<AdminEntity, AdminDto>, IAdminRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public AdminRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<AdminEntity> GetAllAdmins()
        {
            var dbAdmins = _tableBookingContext.Admins.ToList();
            return _mapper.Map<List<AdminEntity>>(dbAdmins);
        }

        public override void Remove(AdminEntity entity)
        {
            var dbEntity = _tableBookingContext.Admins.Where(r => r.Username.Equals(entity.Username)).FirstOrDefault();
            if (dbEntity == null)
            {
                return;
            }

            _tableBookingContext.Admins.Remove(dbEntity);
        }
    }
}