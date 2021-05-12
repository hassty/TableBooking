using Core.Entities.Users;
using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IAdminRepository : IUserRepository
    {
        public IEnumerable<AdminEntity> GetAllAdmins();
    }
}
