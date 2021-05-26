using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class RestaurantOrderOptionsEntity
    {
        public int Id { get; set; }
        public int LatestOrderDate { get; set; }
        public TimeSpan LongestReservationDuration { get; set; }
        public TimeSpan ShortestReservationDuration { get; set; }
        public IList<DayOfWeek> OffDays { get; private set; }

        public RestaurantOrderOptionsEntity() : this(new List<DayOfWeek>())
        {
        }

        public RestaurantOrderOptionsEntity(IList<DayOfWeek> offDays)
        {
            OffDays = offDays;

            LatestOrderDate = 14;
            LongestReservationDuration = new TimeSpan(4, 20, 0);
            ShortestReservationDuration = new TimeSpan(0, 20, 0);
        }
    }
}
