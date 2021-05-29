using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class UpdateMenuItems
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public UpdateMenuItems(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Update(RestaurantEntity restaurant)
        {
            _restaurantRepository.UpdateMenuItems(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}