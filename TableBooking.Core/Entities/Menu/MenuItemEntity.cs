using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Menu
{
    public class MenuItemEntity
    {
        public MenuCategoryEntity Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MenuItemEntity entity &&
                   Category.Equals(entity.Category) &&
                   Description == entity.Description &&
                   Name == entity.Name &&
                   Price == entity.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Category, Description, Name, Price);
        }
    }
}