using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.DataAccess
{
    public interface IUserRepository<UserEntity> : IRepository<UserEntity> where UserEntity : class
    {
        UserEntity GetUserWithUsername(string username);
        bool ContainsUserWithUsername(string username);
    }
}
