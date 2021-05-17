using Core.Dto.Menu;
using Core.Dto.Users;
using System;
using System.Collections.Generic;

namespace Core.Dto
{
    public class OrderDto
    {
        public CustomerDto Customer { get; set; }
        public int Id { get; set; }
        public IList<MenuItemDto> MenuItems { get; private set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationDuration { get; set; }
        public RestaurantDto Restaurant { get; set; }
        public TableDto Table { get; set; }

        public OrderDto()
        {
            MenuItems = new List<MenuItemDto>();
        }
    }
}