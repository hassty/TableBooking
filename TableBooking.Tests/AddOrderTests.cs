using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Entities.Users;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Collections.Generic;
using Xunit;

namespace Core.Tests
{
    public class AddOrderTests
    {
        private const string _username = "customer";
        private readonly AddOrder _addOrder;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public AddOrderTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(AddOrderTests));

            _orderRepository = databaseFixture.OrderRepository;
            _customerRepository = databaseFixture.CustomerRepository;
            _restaurantRepository = databaseFixture.RestaurantRepository;
            _addOrder = new AddOrder(_orderRepository, _customerRepository, _restaurantRepository);

            var customer = new CustomerEntity() { Username = _username };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
        }

        [Fact]
        public void Add_ShouldAddNewOrderToCustomer()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var restaurant = new RestaurantEntity()
            {
                Name = "makdak",
                Address = "addr"
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(restaurant, DateTime.Now.AddDays(1), new TimeSpan(2, 0, 0));

            _addOrder.Add(order, customer.Username);

            Assert.Contains(order, _orderRepository.GetAllOrdersOfCustomer(customer.Username));
        }

        [Fact]
        public void Add_ShouldThrowIfAddingOrderForDayOff()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var offDays = new List<DayOfWeek> {
                DateTime.Now.AddDays(2).DayOfWeek,
                DateTime.Now.AddDays(4).DayOfWeek
            };
            var restaurant = new RestaurantEntity(
                new RestaurantOrderOptionsEntity(offDays)
            )
            {
                Name = "off days makdak",
                Address = "addr",
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(restaurant, DateTime.Now.AddDays(2), new TimeSpan(2, 0, 0));

            Assert.Throws<OrderDateException>(() =>
            {
                _addOrder.Add(order, customer.Username);
            });
        }

        [Fact]
        public void Add_ShouldThrowIfAddingOrderForPastDate()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var restaurant = new RestaurantEntity()
            {
                Name = "past makdak",
                Address = "addr"
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(restaurant, DateTime.Now.AddDays(-1), new TimeSpan(2, 0, 0));

            Assert.Throws<OrderDateException>(() =>
            {
                _addOrder.Add(order, customer.Username);
            });
        }

        [Fact]
        public void Add_ShouldThrowIfAddingOrderForTooLongReservationTime()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var restaurant = new RestaurantEntity(
                new RestaurantOrderOptionsEntity()
                {
                    LongestReservationDuration = new TimeSpan(1, 0, 0)
                })
            {
                Name = "long makdak",
                Address = "addr",
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(
                restaurant,
                DateTime.Now.AddDays(1),
                new TimeSpan(1, 1, 0)
            );

            Assert.Throws<OrderTimeException>(() =>
            {
                _addOrder.Add(order, customer.Username);
            });
        }

        [Fact]
        public void Add_ShouldThrowIfAddingOrderForTooShortReservationTime()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var restaurant = new RestaurantEntity(
                new RestaurantOrderOptionsEntity()
                {
                    ShortestReservationDuration = new TimeSpan(0, 30, 0)
                })
            {
                Name = "short makdak",
                Address = "addr",
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(
                restaurant,
                DateTime.Now.AddDays(1),
                new TimeSpan(0, 29, 0)
            );

            Assert.Throws<OrderTimeException>(() =>
            {
                _addOrder.Add(order, customer.Username);
            });
        }

        [Fact]
        public void Add_ShouldThrowIfAddingOrderPastWorkingHours()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var restaurant = new RestaurantEntity()
            {
                Name = "closed makdak",
                Address = "addr",
                OpenedTill = new TimeSpan(22, 30, 0)
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(
                restaurant,
                new DateTime(2021, 05, 26, 22, 0, 0),
                new TimeSpan(0, 31, 0)
            );

            Assert.Throws<OrderTimeException>(() =>
            {
                _addOrder.Add(order, customer.Username);
            });
        }

        [Fact]
        public void Add_ShouldThrowIfAddingOrderTooFarAwayInTheFuture()
        {
            var customer = _customerRepository.GetUserWithUsername(_username);
            var restaurant = new RestaurantEntity()
            {
                Name = "future makdak",
                Address = "addr",
            };

            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(
                restaurant,
                DateTime.Now.AddDays(restaurant.GetLatestOrderDate()),
                new TimeSpan(2, 0, 0)
            );

            Assert.Throws<OrderDateException>(() =>
            {
                _addOrder.Add(order, customer.Username);
            });
        }
    }
}