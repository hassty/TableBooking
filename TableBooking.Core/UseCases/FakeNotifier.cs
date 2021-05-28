using Core.Contracts;

namespace Core.UseCases
{
    public class FakeNotifier : INotifier
    {
        public void Notify(string receiver, string message)
        {
        }
    }
}