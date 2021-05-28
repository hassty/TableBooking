using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Exceptions;

namespace Core.UseCases
{
    public class CancelOrder
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CancelOrder(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        /// <exception cref="ItemNotFoundException"></exception>
        public void Remove(OrderEntity order, string username)
        {
            var customer = _customerRepository.GetUserWithUsername(username);
            customer.CancelOrder(order);
            try
            {
                _orderRepository.Remove(order);
                _orderRepository.SaveChanges();
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }
    }
}