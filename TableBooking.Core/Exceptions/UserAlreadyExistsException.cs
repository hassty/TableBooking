using System;

namespace Core.Exceptions
{
    public class UserAlreadyExistsException : ApplicationException
    {
        public UserAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
