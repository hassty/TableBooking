using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}