using Core.Contracts.Dto;

namespace WpfUI.Dto
{
    public class AdminDto : IAdminDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}