using Core.Contracts.DataAccess;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases
{
    public class RestaurantInteractor
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantInteractor(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public List<int> GetRestaurantTablesCapacities(string name, string address)
        {
            return _restaurantRepository.GetRestaurantByNameAndAddress(name, address)?.GetTablesCapacities().ToList();
        }
    }
}