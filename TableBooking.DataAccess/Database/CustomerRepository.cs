using AutoMapper;
using Core.Contracts;
using Core.Dto.Users;
using Core.Entities.Users;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Database
{
    public class CustomerRepository : GenericRepository<CustomerEntity, CustomerDto>, ICustomerRespository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public CustomerRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}