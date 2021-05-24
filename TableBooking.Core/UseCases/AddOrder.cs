using Core.Contracts.DataAccess;
using Core.Entities;

namespace Core.UseCases
{
    public class AddOrder
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrder(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Add(OrderEntity order)
        {
            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
        }
    }
}