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
    }
}