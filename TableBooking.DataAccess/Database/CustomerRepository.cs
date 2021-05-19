using Core.Contracts;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database
{
    public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRespository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public CustomerRepository(DbContext context)
            : base(context)
        {
        }
    }
}