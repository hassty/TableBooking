using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class OrderDateException : ApplicationException
    {
        public OrderDateException(string message) : base(message)
        {
        }
    }
}
