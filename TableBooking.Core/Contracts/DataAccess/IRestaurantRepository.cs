using Core.Entities;

namespace Core.Contracts.DataAccess
{
    public interface IRestaurantRepository : IRepository<RestaurantEntity>
    {
        public RestaurantEntity GetRestaurantByName(string name);
    }
}