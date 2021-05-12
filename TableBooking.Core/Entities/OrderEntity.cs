using Core.Entities.Users;
using System;

namespace Core.Entities
{
    public class OrderEntity
    {
        public CustomerEntity Customer { get; set; }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public RestaurantEntity Restaurant { get; set; }
    }
}