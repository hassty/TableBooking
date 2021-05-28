using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class FakeNotifier : INotifier
    {
        public void Notify(string receiver, string message)
        {
        }
    }
}