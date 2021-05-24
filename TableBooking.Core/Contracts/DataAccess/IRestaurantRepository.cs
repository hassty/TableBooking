using Core.Entities;

namespace Core.Contracts.DataAccess
{
    public interface IRestaurantRepository : IRepository<RestaurantEntity>
    {
        public RestaurantEntity GetRestaurantByNameAndAddress(string name, string address);
    }
}