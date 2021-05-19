using Core.Entities.Users;
using System.Collections.Generic;

namespace Core.Contracts.DataAccess
{
    public interface IAdminRepository : IRepository<AdminEntity>
    {
        public IEnumerable<AdminEntity> GetAllAdmins();
    }
}