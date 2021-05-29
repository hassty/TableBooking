using Core.Contracts.DataAccess;
using Core.Entities;
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
            return _tableBookingContext.Orders.Where(o => o.Customer.Username.Equals(username));
        }

        public IEnumerable<OrderEntity> GetAllUnconfirmedOrders()
        {
            return _tableBookingContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Where(o => o.ConfirmedByAdmin == false);
        }

        public bool RestaurantHasOrders(string name, string address)
        {
            return _tableBookingContext.Orders
                .Include(o => o.Restaurant)
                .Any(o => o.Restaurant.Name == name && o.Restaurant.Address == address);
        }
    }
}