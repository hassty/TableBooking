using System.Collections.Generic;


namespace Core.Dto.Users
{
    public class AdminDto : CustomerDto
    {
        public IList<OrderDto> UnconfirmedOrders { get; set; }
    }
}