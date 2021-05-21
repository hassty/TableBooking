using Core.Entities.Users;

namespace Core.Contracts.DataAccess
{
    public interface ICustomerRepository : IUserRepository<CustomerEntity>
    {
    }
}