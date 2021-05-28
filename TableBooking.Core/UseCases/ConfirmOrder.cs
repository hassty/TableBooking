using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Exceptions;

namespace Core.UseCases
{
    public class ConfirmOrder
    {
        private readonly INotifier _notifier;
        private readonly IOrderRepository _orderRepository;

        public ConfirmOrder(IOrderRepository orderRepository, INotifier notifier)
        {
            _orderRepository = orderRepository;
            _notifier = notifier;
        }

        /// <exception cref="NotifierException"></exception>
        public void Confirm(OrderEntity order)
        {
            if (order.ConfirmedByAdmin == true)
            {
                return;
            }

            order.ConfirmedByAdmin = true;
            var message = $"Your order at {order.Restaurant.Name} restaurant on {order.ReservationDate} was confirmed";

            try
            {
                _notifier.Notify(order.Customer.Email, message);
            }
            catch (NotifierException)
            {
                throw;
            }

            _orderRepository.Update(order);
            _orderRepository.SaveChanges();
        }
    }
}