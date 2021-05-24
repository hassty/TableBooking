namespace Core.Contracts
{
    public interface IPasswordProtectionStrategy
    {
        string GetProtectedPassword(string password);
    }
}
