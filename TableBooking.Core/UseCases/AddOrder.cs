using Core.Contracts.DataAccess;
using Core.Entities;
using System;

namespace Core.UseCases
{
    public class AddOrder
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public AddOrder(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IRestaurantRepository restaurantRepository
        )
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public void Add(OrderEntity order, string username, string restaurantName, string address)
        {
            var customer = _customerRepository.GetUserWithUsername(username);
            var restaurant = _restaurantRepository.GetRestaurantByNameAndAddress(restaurantName, address);
            order.Customer = customer;
            order.Restaurant = restaurant;

            if (order.ReservationDate <= DateTime.Now || restaurant.OffDays.Contains(order.ReservationDate.DayOfWeek))
            {
                return;
            }

            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
        }
    }
}