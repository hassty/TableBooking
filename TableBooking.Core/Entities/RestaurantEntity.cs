using System;

namespace Core.Entities
{
    public class RestaurantEntity
    {
        public string Address { get; set; }
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public TimeSpan OpenedFrom { get; set; }
        public TimeSpan OpenedTill { get; set; }

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
    }
}