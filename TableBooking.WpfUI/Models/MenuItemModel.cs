using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Models
{
    public class MenuItemModel : ModelBase
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
