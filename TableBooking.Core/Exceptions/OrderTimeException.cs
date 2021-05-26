using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class OrderTimeException : ApplicationException
    {
        public OrderTimeException(string message) : base(message)
        {
        }
    }
}
