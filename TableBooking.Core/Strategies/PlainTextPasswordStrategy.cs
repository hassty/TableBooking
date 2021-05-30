using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Strategies
{
    public class PlainTextPasswordStrategy : IPasswordProtectionStrategy
    {
        public string GetProtectedPassword(string password)
        {
            return password;
        }
    }
}