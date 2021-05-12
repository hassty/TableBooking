using Core.Entities;

namespace Core.Contracts
{
    public interface IRestaurantRepository : IRepository<RestaurantEntity>
    {
        public RestaurantEntity GetRestaurantByName(string name);
    }
}