using Core.Contracts.Dto;

namespace TableBooking.Models
{
    public class CustomerModel : ICustomerDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}