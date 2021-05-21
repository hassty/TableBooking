using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Dto
{
    public static class AdminMapping
    {
        public static AdminEntity ToEntity(this IAdminDto dto)
        {
            return new AdminEntity
            {
                Username = dto.Username
            };
        }
    }
    public interface IAdminDto : IUserDto
    {
    }
}
