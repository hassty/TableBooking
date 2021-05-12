using System;

namespace Core.Dto
{
    public class RestaurantDto
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan OpenedFrom { get; set; }
        public TimeSpan OpenedTill { get; set; }
    }
}