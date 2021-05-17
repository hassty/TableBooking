using Core.Contracts;
using Core.Entities;
using Core.Entities.Users;
using System.Linq;

namespace Core.UseCases
{
    public class CustomerServiceInteractor
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICustomerRespository _customerRespository;

        public CustomerServiceInteractor(ICustomerRespository customerRespository, IAdminRepository adminRepository)
        {
            _customerRespository = customerRespository;
            _adminRepository = adminRepository;
        }

        public void AddOrder(CustomerEntity customer, OrderEntity order)
        {
            customer.AddOrder(order);
            var admins = _adminRepository.GetAllAdmins().ToList();
            admins.ForEach(a =>
            {
                a.AddUnconfirmedOrder(order);
                _adminRepository.Update(a);
            });

            _adminRepository.SaveChanges();
        }
    }
}