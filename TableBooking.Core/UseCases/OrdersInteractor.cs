using Core.Contracts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class OrdersInteractor
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersInteractor(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void AddOrder(OrderEntity order)
        {
            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
        }
    }
}