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

            CreateMap<RestaurantEntity, RestaurantModel>().ReverseMap();
        }
    }
}