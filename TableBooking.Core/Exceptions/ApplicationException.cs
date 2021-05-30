using System;

namespace Core.Exceptions
{
    public class RestaurantOrdersException : ApplicationException
    {
        public RestaurantOrdersException(string message) : base(message)
        {
        }
    }
}