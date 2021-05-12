using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
    public class TableDto
    {
        public int Capacity { get; set; }
        public int Id { get; set; }
        public RestaurantDto Restaurant { get; set; }
    }
}