using AutoMapper;
using Core.Dto;
using Core.Dto.Menu;
using Core.Dto.Users;
using Core.Entities;
using Core.Entities.Menu;
using Core.Entities.Users;
using System;
using System.Linq;

namespace DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<CustomerEntity, CustomerDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AdminEntity, AdminDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UnconfirmedOrders, opt => opt.Ignore());

            CreateMap<AdminDto, AdminEntity>();

            CreateMap<RestaurantEntity, RestaurantDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TableEntity, TableDto>()
                .ReverseMap();
            CreateMap<OrderEntity, OrderDto>().ReverseMap();

            CreateMap<MenuEntity, MenuDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<MenuCategoryEntity, MenuCategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<MenuItemEntity, MenuItemDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}