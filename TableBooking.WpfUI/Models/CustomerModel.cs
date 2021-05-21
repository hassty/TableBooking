using Core.Contracts.Dto;

namespace WpfUI.Models
{
    public class CustomerModel : ICustomerDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}