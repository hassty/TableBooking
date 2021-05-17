using System.Collections.Generic;

namespace Core.Dto.Menu
{
    public class MenuCategoryDto
    {
        public int Id { get; set; }
        public MenuDto Menu { get; set; }
        public IList<MenuItemDto> MenuItems { get; private set; }
        public string Name { get; set; }
    }
}