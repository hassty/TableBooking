using System;

namespace Core.Entities.Menu
{
    public class MenuItemEntity
    {
        public MenuCategoryEntity Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }

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