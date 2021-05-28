using Core.Contracts.DataAccess;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases
{
    public class GetAllUnconfirmedOrders
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllUnconfirmedOrders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderEntity> GetOrders()
        {
            return _orderRepository.GetAllUnconfirmedOrders().ToList();
        }
    }
}