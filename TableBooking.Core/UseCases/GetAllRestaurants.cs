using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.UseCases
{
    public class GetAllRestaurants
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public GetAllRestaurants(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public List<RestaurantEntity> GetRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }
    }
}