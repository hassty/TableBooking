using Core.Entities;
using System.Collections.Generic;

namespace Core.Contracts.DataAccess
{
    public interface IOrderRepository : IRepository<OrderEntity>
    {
        IEnumerable<OrderEntity> GetAllOrdersOfCustomer(string username);

        IEnumerable<OrderEntity> GetAllUnconfirmedOrders();

        bool RestaurantHasOrders(string name, string address);
    }
}