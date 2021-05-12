using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto.Menu
{
    public class MenuDto
    {
        public IList<MenuCategoryDto> Categories { get; set; }
        public int Id { get; set; }
    }
}