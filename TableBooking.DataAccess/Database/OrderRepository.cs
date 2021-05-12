using AutoMapper;
using Core.Contracts;
using Core.Dto;
using Core.Entities;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Database
{
    public class OrderRepository : GenericRepository<OrderEntity, OrderDto>, IOrderRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public OrderRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}