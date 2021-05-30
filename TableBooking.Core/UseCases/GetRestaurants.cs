using Core.Contracts.DataAccess;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases
{
    public class GetRestaurants
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public GetRestaurants(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public List<RestaurantEntity> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }

        public RestaurantEntity GetRestaurantByNameAndAddress(string name, string address)
        {
            return _restaurantRepository.GetRestaurantByNameAndAddress(name, address);
        }
    }
}