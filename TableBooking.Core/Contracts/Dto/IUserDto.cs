using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Dto
{
    public interface IUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
