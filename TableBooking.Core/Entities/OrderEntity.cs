using Core.Entities.Menu;
using Core.Entities.Users;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class OrderEntity
    {
        public CustomerEntity Customer { get; set; }
        public int Id { get; set; }
        public IList<MenuItemEntity> MenuItems { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationDuration { get; set; }
        public RestaurantEntity Restaurant { get; set; }
        public TableEntity Table { get; set; }

        public OrderEntity()
        {
            MenuItems = new List<MenuItemEntity>();
        }
    }
}