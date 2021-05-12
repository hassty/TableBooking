using System.Collections.Generic;

namespace Core.Dto.Users
{
    public class CustomerDto : UserDto
    {
        public string Email { get; set; }
        public IList<OrderDto> Orders { get; set; }
    }
}