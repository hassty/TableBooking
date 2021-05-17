using AutoMapper;
using Core.Contracts;
using Core.Dto;
using Core.Entities;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override void Add(OrderEntity order)
        {
            var newOrder = new OrderDto();
            _mapper.Map(order, newOrder);
            _tableBookingContext.Orders.Add(newOrder);
        }

        public IEnumerable<OrderEntity> GetAllOrdersOfCustomer(string username)
        {
            var existsingOrders = _tableBookingContext.Orders.Where(o => o.Customer.Username.Equals(username)).AsEnumerable();
            return _mapper.Map<IEnumerable<OrderEntity>>(existsingOrders);
        }
    }
}