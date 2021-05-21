using Core.Contracts.Dto;

namespace WpfUI.odels
{
    public class CustomerModel : ICustomerDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}