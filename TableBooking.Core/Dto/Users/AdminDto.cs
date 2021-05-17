using System.Collections.Generic;

namespace Core.Dto.Users
{
    public class AdminDto : UserDto
    {
        public IList<OrderDto> UnconfirmedOrders { get; private set; }

        public AdminDto()
        {
            UnconfirmedOrders = new List<OrderDto>();
        }
    }
}