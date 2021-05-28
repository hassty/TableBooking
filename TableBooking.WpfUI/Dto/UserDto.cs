using Core.Contracts.Dto;

namespace WpfUI.Dto
{
    public class CustomerDto : ICustomerDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}