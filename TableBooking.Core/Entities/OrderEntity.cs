using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class OrderEntity
    {
        public bool ConfirmedByAdmin { get; set; }
        public CustomerEntity Customer { get; set; }
        public int Id { get; set; }
        public IList<MenuItemEntity> MenuItems { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int PartySize { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationDuration { get; set; }
        public RestaurantEntity Restaurant { get; set; }
        public string Status => ConfirmedByAdmin ? "Confirmed" : "Waiting";

        public decimal TotalPrice
        {
            get
            {
                if (MenuItems == null)
                {
                    return 0;
                }

                return MenuItems.Select(o => o.Price).Sum();
            }
        }

        public OrderEntity(
            RestaurantEntity restaurant,
            DateTime reservationDate,
            TimeSpan reservationDuration,
            int partySize = 1
        )
        {
            ConfirmedByAdmin = false;
            MenuItems = new List<MenuItemEntity>();
            OrderDate = DateTime.Now;
            PartySize = partySize;

            Restaurant = restaurant;
            ReservationDate = reservationDate;
            ReservationDuration = reservationDuration;
        }

        public OrderEntity()
        {
            MenuItems = new List<MenuItemEntity>();
        }

        public override bool Equals(object obj)
        {
            return obj is OrderEntity entity &&
                   EqualityComparer<CustomerEntity>.Default.Equals(Customer, entity.Customer) &&
                   Id == entity.Id &&
                   OrderDate == entity.OrderDate &&
                   PartySize == entity.PartySize &&
                   ReservationDate == entity.ReservationDate &&
                   ReservationDuration.Equals(entity.ReservationDuration) &&
                   EqualityComparer<RestaurantEntity>.Default.Equals(Restaurant, entity.Restaurant);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Customer, Id, OrderDate, PartySize, ReservationDate, ReservationDuration, Restaurant);
        }

        public TimeSpan GetReservationTimeEnding()
        {
            return ReservationDate.Add(ReservationDuration).TimeOfDay;
        }
    }
}