using System;

namespace Core.Exceptions
{
    public class ItemAlreadyExistsException : ApplicationException
    {
        public ItemAlreadyExistsException(string message) : base(message)
        {
        }
    }
}