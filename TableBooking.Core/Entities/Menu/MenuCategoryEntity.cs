using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Menu
{
    public class MenuCategoryEntity
    {
        public MenuEntity Menu { get; set; }
        public IList<MenuItemEntity> MenuItems { get; private set; }
        public string Name { get; set; }

        public MenuCategoryEntity()
        {
            MenuItems = new List<MenuItemEntity>();
        }
    }
}