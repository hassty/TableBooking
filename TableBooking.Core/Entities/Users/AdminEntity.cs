using System.Collections.Generic;

namespace Core.Entities.Users
{
    public class AdminEntity : UserEntity
    {
        public IList<OrderEntity> UnconfirmedOrders { get; private set; }

        public AdminEntity()
        {
            UnconfirmedOrders = new List<OrderEntity>();
        }

        public void AddUnconfirmedOrder(OrderEntity order)
        {
            UnconfirmedOrders.Add(order);
        }
    }
}