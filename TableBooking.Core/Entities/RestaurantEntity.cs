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

        public override bool Equals(object obj)
        {
            return obj is RestaurantEntity entity &&
                   Address == entity.Address &&
                   City == entity.City &&
                   Name == entity.Name &&
                   OpenedFrom.Equals(entity.OpenedFrom) &&
                   OpenedTill.Equals(entity.OpenedTill);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, City, Name, OpenedFrom, OpenedTill);
        }
    }
}