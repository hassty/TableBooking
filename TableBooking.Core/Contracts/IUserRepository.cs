using Core.Entities.Users;

namespace Core.Contracts
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetUserWithUsername(string username);
    }
}