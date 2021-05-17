using System.Collections.Generic;

namespace Core.Dto.Menu
{
    public class MenuDto
    {
        public IList<MenuCategoryDto> Categories { get; set; }
        public int Id { get; set; }
    }
}