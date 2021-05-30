using Core.Contracts.DataAccess;

namespace Core.UseCases
{
    public class CancelRestaurantChanges
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public CancelRestaurantChanges(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Cancel()
        {
            _restaurantRepository.Rollback();
            _restaurantRepository.SaveChanges();
        }
    }
}