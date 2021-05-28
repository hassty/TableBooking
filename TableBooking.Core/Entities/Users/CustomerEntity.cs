using System;
using System.Collections.Generic;

namespace Core.Entities.Users
{
    public class CustomerEntity : UserEntity
    {
        public string Email { get; set; }
        public IList<OrderEntity> Orders { get; private set; }

        public CustomerEntity()
        {
            Orders = new List<OrderEntity>();
        }

        public void AddOrder(OrderEntity order)
        {
            Orders.Add(order);
        }

        public void CancelOrder(OrderEntity order)
        {
            Orders.Remove(order);
        }

        public override bool Equals(object obj)
        {
            return obj is CustomerEntity entity &&
                   PasswordHash == entity.PasswordHash &&
                   Salt == entity.Salt &&
                   Username == entity.Username &&
                   Email == entity.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PasswordHash, Salt, Username, Email, Orders);
        }

        public IEnumerable<OrderEntity> GetOrders()
        {
            return Orders;
        }
    }
}