namespace Core.Contracts
{
    public interface INotifier
    {
        void Notify(string receiver, string message);
    }
}