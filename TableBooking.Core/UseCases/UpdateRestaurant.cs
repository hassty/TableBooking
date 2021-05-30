using Core.Contracts.DataAccess;
using Core.Entities;

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