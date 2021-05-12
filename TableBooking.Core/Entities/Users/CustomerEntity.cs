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
    }
}