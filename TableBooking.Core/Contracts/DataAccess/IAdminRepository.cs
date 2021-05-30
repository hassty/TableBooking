using Core.Entities.Users;

namespace Core.Contracts.DataAccess
{
    public interface IAdminRepository : IUserRepository<AdminEntity>
    {
    }
}