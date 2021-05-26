using Core.Entities.Menu;
using Core.Entities.Users;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class OrderEntity
    {
        public bool ConfirmedByAdmin { get; private set; }
        public CustomerEntity Customer { get; set; }
        public int Id { get; set; }
        public IList<MenuItemEntity> MenuItems { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationDuration { get; set; }
        public RestaurantEntity Restaurant { get; set; }
        public TableEntity Table { get; set; }

        public OrderEntity(DateTime reservationDate, TimeSpan reservationDuration)
        {
            ConfirmedByAdmin = false;
            MenuItems = new List<MenuItemEntity>();
            OrderDate = DateTime.Now;

            ReservationDate = reservationDate;
            ReservationDuration = reservationDuration;
        }

        public void Confirm(AdminEntity admin)
        {
            admin.ConfirmOrder(this);
            ConfirmedByAdmin = true;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderEntity entity &&
                   EqualityComparer<CustomerEntity>.Default.Equals(Customer, entity.Customer) &&
                   Id == entity.Id &&
                   OrderDate == entity.OrderDate &&
                   EqualityComparer<TableEntity>.Default.Equals(Table, entity.Table);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Customer, Id, OrderDate, Table);
        }

        public TimeSpan GetReservationTimeEnding()
        {
            return ReservationDate.Add(ReservationDuration).TimeOfDay;
        }
    }
}