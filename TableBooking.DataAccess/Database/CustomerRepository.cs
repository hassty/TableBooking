using Core.Contracts.DataAccess;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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

        public bool ContainsCustomerWithUsername(string username)
        {
            return GetCustomerWithUsername(username) != null;
        }

        public CustomerEntity GetCustomerWithUsername(string username)
        {
            return _tableBookingContext.Customers.FirstOrDefault(c => c.Username == username);
        }
    }
}