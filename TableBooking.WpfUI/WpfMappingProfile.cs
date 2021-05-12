using AutoMapper;
using Core.Entities;
using Core.Entities.Users;
using TableBooking.Models;
using WpfUI.Models;

namespace WpfUI
{
    public class WpfMappingProfile : Profile
    {
        public WpfMappingProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();
            CreateMap<RestaurantEntity, RestaurantModel>().ReverseMap();
        }
    }
}