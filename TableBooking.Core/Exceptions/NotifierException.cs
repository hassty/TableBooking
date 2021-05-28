using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class NotifierException : ApplicationException
    {
        public NotifierException(string message) : base(message)
        {
        }
    }
}