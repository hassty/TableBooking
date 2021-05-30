using Core.Contracts.DataAccess;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases
{
    public class GetCustomerOrders
    {
        private readonly IOrderRepository _orderRepository;

        public GetCustomerOrders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderEntity> GetAllOrders(string username)
        {
            return _orderRepository.GetAllOrdersOfCustomer(username).ToList();
        }

        public List<OrderEntity> GetUnconfirmedOrders(string username)
        {
            return _orderRepository.GetAllOrdersOfCustomer(username).Where(o => o.ConfirmedByAdmin == false).ToList();
        }
    }
}