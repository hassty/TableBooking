using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class UpdateRestaurant
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public UpdateRestaurant(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Update(RestaurantEntity restaurant)
        {
            _restaurantRepository.Update(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}