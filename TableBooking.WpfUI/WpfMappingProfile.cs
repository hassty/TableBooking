using AutoMapper;
using Core.Entities;
using Core.Entities.Users;
using WpfUI.Models;

namespace WpfUI
{
    public class WpfMappingProfile : Profile
    {
        public WpfMappingProfile()
        {
            CreateMap<RestaurantEntity, RestaurantModel>()
                .ReverseMap();
            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<CustomerEntity, CustomerModel>()
                .IncludeBase<UserEntity, UserModel>();
            CreateMap<CustomerModel, CustomerEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());
            CreateMap<OrderEntity, OrderModel>()
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(o => o.Restaurant.Name))
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(
                    dest => dest.RestaurantAddress,
                    opt => opt.MapFrom(o => $"{o.Restaurant.City} {o.Restaurant.Address}")
                );
        }
    }
}