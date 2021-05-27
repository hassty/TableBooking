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
        public string Name { get; set; }
        public TimeSpan OpenedFrom { get; set; }
        public TimeSpan OpenedTill { get; set; }
        public RestaurantOrderOptionsEntity OrderOptions { get; set; }
        public IList<TableEntity> Tables { get; private set; }

        public RestaurantEntity() : this(new RestaurantOrderOptionsEntity())
        {
        }

        public RestaurantEntity(RestaurantOrderOptionsEntity orderOptions)
        {
            Tables = new List<TableEntity>();
            OrderOptions = orderOptions;
            OpenedTill = OpenedFrom = new TimeSpan(0, 0, 0);
        }

        public void AddTable(int capacity)
        {
            Tables.Add(new TableEntity() { Restaurant = this, Capacity = capacity });
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

        public IList<DayOfWeek> GetOffDays()
        {
            return OrderOptions.OffDays;
        }

        public TimeSpan GetShortestReservationDuration()
        {
            return OrderOptions.ShortestReservationDuration;
        }

        public IEnumerable<int> GetTablesCapacities()
        {
            return Tables.Select(t => t.Capacity).Distinct().OrderBy(c => c);
        }

        public bool IsAllDayOpened()
        {
            return OpenedFrom == OpenedTill;
        }

        public bool IsOffDay(DateTime date)
        {
            return OrderOptions.OffDays.Contains(date.DayOfWeek);
        }
    }
}