using System;
using System.Collections.Generic;

namespace Core.Entities.Menu
{
    public class MenuCategoryEntity
    {
        public MenuEntity Menu { get; set; }
        public int Id { get; set; }
        public IList<MenuItemEntity> MenuItems { get; private set; }
        public string Name { get; set; }

        public MenuCategoryEntity()
        {
            MenuItems = new List<MenuItemEntity>();
        }

        public override bool Equals(object obj)
        {
            return obj is MenuCategoryEntity entity &&
                   Id == entity.Id &&
                   Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}