using AutoMapper;
using Core.Contracts;
using Core.Dto;
using Core.Entities;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Database
{
    public class RestaurantRepository : GenericRepository<RestaurantEntity, RestaurantDto>, IRestaurantRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public RestaurantRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public RestaurantEntity GetRestaurantByName(string name)
        {
            var dbEntity = _tableBookingContext.Restaurants.Where(r => r.Name.Equals(name)).FirstOrDefault();
            return _mapper.Map<RestaurantEntity>(dbEntity);
        }

        public override void Remove(RestaurantEntity entity)
        {
            var dbEntity = _tableBookingContext.Restaurants.Where(r => r.Name.Equals(entity.Name)).FirstOrDefault();
            _tableBookingContext.Restaurants.Remove(dbEntity);
        }
    }
}