using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Menu
{
    public class MenuEntity
    {
        public IList<MenuCategoryEntity> Categories { get; private set; }

        public MenuEntity()
        {
            Categories = new List<MenuCategoryEntity>();
        }
    }
}