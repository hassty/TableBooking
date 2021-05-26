using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Models
{
    public class OrderModel : ModelBase
    {
        public bool ConfirmedByAdmin { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationDuration { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantName { get; set; }
        public string Status { get => ConfirmedByAdmin ? "Confirmed" : "Waiting"; }
        public int TableCapacity { get; set; }
    }
}