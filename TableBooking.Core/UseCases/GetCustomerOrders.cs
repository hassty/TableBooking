using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.UseCases
{
    public class GetCustomerOrders
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerOrders(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<OrderEntity> GetAllOrders(string username)
        {
            return _customerRepository.GetCustomerOrders(username).ToList();
        }

        public List<OrderEntity> GetConfirmedOrders(string username)
        {
            var customer = _customerRepository.GetUserWithUsername(username);
            return customer.GetOrders().Where(o => o.ConfirmedByAdmin == true).ToList();
        }

        public List<OrderEntity> GetUnconfirmedOrders(string username)
        {
            return _customerRepository.GetCustomerOrders(username).Where(o => o.ConfirmedByAdmin == false).ToList();
        }
    }
}