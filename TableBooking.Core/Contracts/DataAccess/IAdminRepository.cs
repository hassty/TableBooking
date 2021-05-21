using Core.Entities.Users;
using System.Collections.Generic;

namespace Core.Contracts.DataAccess
{
    public interface IAdminRepository : IUserRepository<AdminEntity>
    {
    }
}