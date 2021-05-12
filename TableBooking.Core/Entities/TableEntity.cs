using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class TableEntity
    {
        public int Capacity { get; set; }
        public int Id { get; set; }
        public RestaurantEntity Restaurant { get; set; }
    }
}