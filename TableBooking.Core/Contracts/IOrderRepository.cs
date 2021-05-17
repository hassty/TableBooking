using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IOrderRepository : IRepository<OrderEntity>
    {
        IEnumerable<OrderEntity> GetAllOrdersOfCustomer(string username);
    }
}