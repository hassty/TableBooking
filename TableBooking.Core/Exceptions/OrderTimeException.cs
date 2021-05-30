using System;

namespace Core.Exceptions
{
    public class OrderTimeException : ApplicationException
    {
        public OrderTimeException(string message) : base(message)
        {
        }
    }
}
