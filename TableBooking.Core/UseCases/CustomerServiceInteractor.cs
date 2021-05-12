using Core.Contracts;
using Core.Entities;
using Core.Entities.Users;
using System.Collections.Generic;

namespace Core.UseCases
{
    public class CustomerServiceInteractor
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IAdminRepository _adminRepository;

        public CustomerServiceInteractor(ICustomerRespository customerRespository, IAdminRepository adminRepository)
        {
            _customerRespository = customerRespository;
            _adminRepository = adminRepository;
        }

        public void AddOrder(CustomerEntity customer, OrderEntity order)
        {
            customer.AddOrder(order);
        }
    }
}
