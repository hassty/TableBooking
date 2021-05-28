using Core.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Dto
{
    public class CustomerDto : ICustomerDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}