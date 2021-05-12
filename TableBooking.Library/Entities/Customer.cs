using System;
using System.Collections.Generic;
using System.Text;

namespace TableBooking.Core.Entities
{
    public class Customer : User
    {
        public string Email { get; set; }
        public IList<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}