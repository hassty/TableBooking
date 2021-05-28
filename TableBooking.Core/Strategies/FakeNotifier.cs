using Core.Contracts;

namespace Core.Strategies
{
    public class FakeNotifier : INotifier
    {
        public void Notify(string receiver, string message)
        {
        }
    }
}