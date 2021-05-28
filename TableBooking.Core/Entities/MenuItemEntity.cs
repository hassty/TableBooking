using System;

namespace Core.Entities
{
    public class MenuItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MenuItemEntity entity &&
                   Id == entity.Id &&
                   Name == entity.Name &&
                   Price == entity.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Price);
        }
    }
}