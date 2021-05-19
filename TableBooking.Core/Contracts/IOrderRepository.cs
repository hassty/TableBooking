using Core.Entities;
using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IOrderRepository : IRepository<OrderEntity>
    {
        IEnumerable<OrderEntity> GetAllOrdersOfCustomer(string username);
        IEnumerable<OrderEntity> GetAllUnconfirmedOrders();
    }
}