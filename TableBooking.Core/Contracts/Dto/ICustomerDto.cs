using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Dto
{
    public static class CustomerMapping
    {
        public static CustomerEntity ToEntity(this ICustomerDto dto)
        {
            return new CustomerEntity
            {
                Username = dto.Username,
                Email = dto.Email
            };
        }
    }
    public interface ICustomerDto : IUserDto
    {
        public string Email { get; set; }

    }
}
