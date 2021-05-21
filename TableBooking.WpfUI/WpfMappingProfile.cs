using AutoMapper;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Entities.Users;
using WpfUI.Models;

namespace WpfUI
{
    public class WpfMappingProfile : Profile
    {
        public WpfMappingProfile()
        {

            CreateMap<RestaurantEntity, RestaurantModel>().ReverseMap();
            CreateMap<CustomerEntity, CustomerModel>().ReverseMap();
        }
    }
}