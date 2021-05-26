using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Models
{
    public class OrderModel : ModelBase
    {
        public DateTime OrderDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationDuration { get; set; }
        public RestaurantModel Restaurant { get; set; }
        public int TableCapacity { get; set; }
        public bool ConfirmedByAdmin { get; set; }
    }
}
