using AutoMapper;
using Core.Dto;
using Core.Dto.Users;
using Core.Entities;
using Core.Entities.Users;

namespace DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<RestaurantEntity, RestaurantDto>().ReverseMap();
        }
    }
}