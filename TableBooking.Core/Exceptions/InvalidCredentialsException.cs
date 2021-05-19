using System;

namespace Core.Exceptions
{
    public class InvalidCredentialsException : ApplicationException
    {
        public InvalidCredentialsException(string message) : base(message)
        {
        }
    }
}
