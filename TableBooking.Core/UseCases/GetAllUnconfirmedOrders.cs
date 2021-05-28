using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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