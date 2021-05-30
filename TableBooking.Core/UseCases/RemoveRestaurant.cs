using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Exceptions;

namespace Core.UseCases
{
    public class RemoveRestaurant
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public RemoveRestaurant(IRestaurantRepository restaurantRepository, IOrderRepository orderRepository)
        {
            _restaurantRepository = restaurantRepository;
            _orderRepository = orderRepository;
        }

        /// <exception cref="RestaurantOrdersException"></exception>
        /// <exception cref="ItemNotFoundException"></exception>
        public void Remove(RestaurantEntity restaurant)
        {
            var name = restaurant.Name;
            var address = restaurant.Address;

            if (_restaurantRepository.ContainsRestaurant(name, address) == false)
            {
                throw new ItemNotFoundException($"Restaurant {name} {address} was not found");
            }
            if (_orderRepository.RestaurantHasOrders(name, address))
            {
                throw new RestaurantOrdersException($"Restaurant {name} {address} has orders");
            }

            _restaurantRepository.Remove(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}