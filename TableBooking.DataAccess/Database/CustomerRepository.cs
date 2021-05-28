using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Database
{
    public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public CustomerRepository(DbContext context)
            : base(context)
        {
        }

        public bool ContainsUserWithUsername(string username)
        {
            return GetUserWithUsername(username) != null;
        }

        public IEnumerable<OrderEntity> GetCustomerOrders(string username)
        {
            return _tableBookingContext.Orders
                .Include(o => o.Customer)
                .Where(o => o.Customer.Username == username);
        }

        public CustomerEntity GetUserWithUsername(string username)
        {
            return _tableBookingContext.Customers.FirstOrDefault(c => c.Username == username);
        }
    }
}