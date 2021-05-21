using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IPasswordProtectionStrategy
    {
        string GetProtectedPassword(string password);
    }
}
