using Core.Entities;
using Core.Entities.Users;
using System.Collections.Generic;

namespace Core.Contracts.DataAccess
{
    public interface ICustomerRepository : IUserRepository<CustomerEntity>
    {
        public IEnumerable<OrderEntity> GetCustomerOrders(string username);
    }
}