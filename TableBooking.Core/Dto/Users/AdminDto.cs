using System.Collections.Generic;

namespace Core.Dto.Users
{
    public class AdminDto : UserDto
    {
        public IList<OrderDto> UnconfirmedOrders { get; set; }
    }
}