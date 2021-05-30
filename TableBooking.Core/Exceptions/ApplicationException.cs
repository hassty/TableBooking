using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class RestaurantOrdersException : ApplicationException
    {
        public RestaurantOrdersException(string message) : base(message)
        {
        }
    }
}