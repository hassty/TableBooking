using Core.Contracts.DataAccess;
using Core.Entities;

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