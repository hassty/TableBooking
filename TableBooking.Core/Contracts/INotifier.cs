using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface INotifier
    {
        void Notify(string receiver, string message);
    }
}