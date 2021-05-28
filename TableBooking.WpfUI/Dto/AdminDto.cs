using Core.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Dto
{
    public class AdminDto : IAdminDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}