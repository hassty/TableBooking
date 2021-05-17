using AutoMapper;
using Core.Contracts;
using Core.Dto;
using Core.Dto.Users;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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
            var existingAdmins = _tableBookingContext.Admins.AsEnumerable();
            return _mapper.Map<IEnumerable<AdminEntity>>(existingAdmins);
        }

        public override void Remove(AdminEntity admin)
        {
            var existingAdmin = _tableBookingContext.Admins.FirstOrDefault(a => a.Username.Equals(admin.Username));
            if (existingAdmin == null)
            {
                return;
            }

            _tableBookingContext.Admins.Remove(existingAdmin);
        }

        public override void Update(AdminEntity admin)
        {
            var existingAdmin = _tableBookingContext.Admins.FirstOrDefault(a => a.Username.Equals(admin.Username));
            _mapper.Map(admin, existingAdmin);

            foreach (var order in admin.UnconfirmedOrders)
            {
                var existingOrder = existingAdmin.UnconfirmedOrders.FirstOrDefault(o => o.Id == order.Id);

                if (existingOrder == null)
                {
                    var newOrder = _mapper.Map<OrderDto>(order);
                    _tableBookingContext.Add(newOrder);
                    existingAdmin.UnconfirmedOrders.Add(newOrder);
                }
                else
                {
                    _mapper.Map(order, existingAdmin.UnconfirmedOrders.FirstOrDefault(o => o.Id == order.Id));
                }
            }
        }
    }
}