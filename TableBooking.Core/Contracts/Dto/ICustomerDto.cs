using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Dto
{
    public interface ICustomerDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
