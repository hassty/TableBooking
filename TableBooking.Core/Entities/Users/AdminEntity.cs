using System;
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

        public override bool Equals(object obj)
        {
            return obj is AdminEntity entity &&
                   PasswordHash == entity.PasswordHash &&
                   Salt == entity.Salt &&
                   Username == entity.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PasswordHash, Salt, Username, UnconfirmedOrders);
        }
    }
}