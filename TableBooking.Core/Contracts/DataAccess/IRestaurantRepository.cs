using Core.Entities;

namespace Core.Contracts.DataAccess
{
    public interface IRestaurantRepository : IRepository<RestaurantEntity>
    {
        bool ContainsRestaurant(string name, string address);

        RestaurantEntity GetRestaurantByNameAndAddress(string name, string address);
    }
}