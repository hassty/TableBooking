using Core.Contracts.DataAccess;
using Core.Entities;

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

        public void RemoveOrder(OrderEntity order)
        {
            _orderRepository.Remove(order);
            _orderRepository.SaveChanges();
        }
    }
}