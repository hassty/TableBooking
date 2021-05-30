using System;

namespace Core.Exceptions
{
    public class OrderDateException : ApplicationException
    {
        public OrderDateException(string message) : base(message)
        {
        }
    }
}
