using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class RestaurantEntity
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int Id { get; set; }
        public IList<MenuItemEntity> MenuItems { get; set; }
        public string Name { get; set; }
        public TimeSpan OpenedFrom { get; set; }
        public TimeSpan OpenedTill { get; set; }
        public RestaurantOrderOptionsEntity OrderOptions { get; set; }

        public RestaurantEntity() : this(new RestaurantOrderOptionsEntity())
        {
            MenuItems = new List<MenuItemEntity>();
        }

        public RestaurantEntity(RestaurantOrderOptionsEntity orderOptions)
        {
            MenuItems = new List<MenuItemEntity>();
            OrderOptions = orderOptions;
            OpenedTill = OpenedFrom = new TimeSpan(0, 0, 0);
        }

        public override bool Equals(object obj)
        {
            return obj is RestaurantEntity entity &&
                   Address == entity.Address &&
                   Id == entity.Id &&
                   City == entity.City &&
                   Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, Id, City, Name);
        }

        public int GetLatestOrderDate()
        {
            return OrderOptions.LatestOrderDate;
        }

        public TimeSpan GetLongestReservationDuration()
        {
            return OrderOptions.LongestReservationDuration;
        }

        public IList<DateTime> GetOffDates()
        {
            return Enumerable.Range(0, OrderOptions.LatestOrderDate + 1).Select(d => DateTime.Now.AddDays(d)).ToList();
        }

        public IEnumerable<int> GetPartySizes()
        {
            return Enumerable.Range(1, OrderOptions.MaxPartySize + 1);
        }

        public TimeSpan GetShortestReservationDuration()
        {
            return OrderOptions.ShortestReservationDuration;
        }

        public bool IsAllDayOpened()
        {
            return OpenedFrom == OpenedTill;
        }
    }
}