using AutoMapper;
using Core.Contracts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Database
{
    public class CustomerRepository : UserRepository, ICustomerRespository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public CustomerRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}
