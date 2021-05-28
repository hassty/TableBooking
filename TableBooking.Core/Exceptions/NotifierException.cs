using System;

namespace Core.Exceptions
{
    public class NotifierException : ApplicationException
    {
        public NotifierException(string message) : base(message)
        {
        }
    }
}