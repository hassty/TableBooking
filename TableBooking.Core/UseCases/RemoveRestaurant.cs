using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class RemoveRestaurant
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RemoveRestaurant(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        /// <exception cref="ItemNotFoundException"></exception>
        public void Remove(RestaurantEntity restaurant)
        {
            if (_restaurantRepository.ContainsRestaurant(restaurant.Name, restaurant.Address) == false)
            {
                throw new ItemNotFoundException($"{restaurant.Name} {restaurant.Address} was not found");
            }

            _restaurantRepository.Remove(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}