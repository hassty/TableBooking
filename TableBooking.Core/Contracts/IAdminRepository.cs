using Core.Entities.Users;
using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IAdminRepository : IRepository<AdminEntity>
    {
        public IEnumerable<AdminEntity> GetAllAdmins();
    }
}