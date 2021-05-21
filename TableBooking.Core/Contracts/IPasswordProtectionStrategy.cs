using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IPasswordProtectionStrategy
    {
        (int, string) HashAndSaltPassword(string password);
    }
}
