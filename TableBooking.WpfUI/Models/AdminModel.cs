using Core.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Models
{
    public class AdminModel : IAdminDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
