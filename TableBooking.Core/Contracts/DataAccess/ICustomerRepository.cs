using Core.Entities.Users;

namespace Core.Contracts.DataAccess
{
    public interface ICustomerRepository : IRepository<CustomerEntity>
    {
        bool ContainsCustomerWithUsername(string username);
        CustomerEntity GetCustomerWithUsername(string username);
    }
}