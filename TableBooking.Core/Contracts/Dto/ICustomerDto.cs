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
    public interface ICustomerDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
