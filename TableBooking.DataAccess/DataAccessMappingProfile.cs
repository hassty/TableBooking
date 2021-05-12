using AutoMapper;
using Core.Dto;
using Core.Dto.Menu;
using Core.Dto.Users;
using Core.Entities;
using Core.Entities.Menu;
using Core.Entities.Users;

namespace DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<CustomerEntity, CustomerDto>().ReverseMap();
            CreateMap<AdminEntity, AdminDto>().ReverseMap();

            CreateMap<RestaurantEntity, RestaurantDto>().ReverseMap();
            CreateMap<TableEntity, TableDto>().ReverseMap();
            CreateMap<OrderEntity, OrderDto>().ReverseMap();

            CreateMap<MenuEntity, MenuDto>().ReverseMap();
            CreateMap<MenuCategoryEntity, MenuCategoryDto>().ReverseMap();
            CreateMap<MenuItemEntity, MenuItemDto>().ReverseMap();
        }
    }
}