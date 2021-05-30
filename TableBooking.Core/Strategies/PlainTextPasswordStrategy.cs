using Core.Contracts;

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