using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Exceptions;
using System;

namespace Core.UseCases
{
    public class AddOrder
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
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

        private void ValidateOrderDate(OrderEntity order, RestaurantEntity restaurant)
        {
            if (order.ReservationDate <= DateTime.Now)
            {
                throw new OrderDateException("Can't add order for the past date");
            }
            if (restaurant.IsOffDay(order.ReservationDate))
            {
                throw new OrderDateException("Can't add order for a day off");
            }
            if (order.ReservationDate.Date >= DateTime.Now.Date.AddDays(restaurant.GetLatestOrderDate()))
            {
                throw new OrderDateException("Can't add order for too far in the future");
            }
        }

        private void ValidateOrderTime(OrderEntity order, RestaurantEntity restaurant)
        {
            if (order.ReservationDuration < restaurant.GetShortestReservationDuration())
            {
                throw new OrderTimeException("Too short reservation duration");
            }
            if (order.ReservationDuration > restaurant.GetLongestReservationDuration())
            {
                throw new OrderTimeException("Too long reservation duration");
            }
            if (restaurant.IsAllDayOpened() == false && order.GetReservationTimeEnding() > restaurant.OpenedTill)
            {
                throw new OrderTimeException("Choose shorter reservation duration or earlier reservation time");
            }
        }

        /// <exception cref="OrderTimeException"></exception>
        /// <exception cref="OrderDateException"></exception>
        public void Add(OrderEntity order, string username)
        {
            var customer = _customerRepository.GetUserWithUsername(username);
            order.Customer = customer;

            var restaurant = order.Restaurant;

            ValidateOrderTime(order, restaurant);
            ValidateOrderDate(order, restaurant);

            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
        }
    }
}