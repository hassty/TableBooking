using System;

namespace Core.Entities
{
    public class RestaurantEntity
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public TimeSpan OpenedFrom { get; set; }
        public TimeSpan OpenedTill { get; set; }
    }
}