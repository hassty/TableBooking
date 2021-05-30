using System;

namespace Core.Entities
{
    public class RestaurantOrderOptionsEntity
    {
        public int Id { get; set; }
        public int LatestOrderDate { get; set; }
        public TimeSpan LongestReservationDuration { get; set; }
        public int MaxPartySize { get; set; }
        public TimeSpan ShortestReservationDuration { get; set; }

        public RestaurantOrderOptionsEntity()
        {
            LatestOrderDate = 14;
            MaxPartySize = 3;
            LongestReservationDuration = new TimeSpan(4, 20, 0);
            ShortestReservationDuration = new TimeSpan(0, 20, 0);
        }
    }
}