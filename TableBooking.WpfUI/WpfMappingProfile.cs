using AutoMapper;
using Core.Entities;
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