using Core.Contracts.DataAccess;
using Core.Entities;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public OrderRepository(DbContext context)
            : base(context)
        {
        }


        public IEnumerable<OrderEntity> GetAllOrdersOfCustomer(string username)
        {
            return _tableBookingContext.Orders.Where(o => o.Customer.Username.Equals(username)).AsEnumerable();
        }

        public IEnumerable<OrderEntity> GetAllUnconfirmedOrders()
        {
            return _tableBookingContext.Orders.Where(o => o.ConfirmedByAdmin == false).AsEnumerable();
        }
    }
}