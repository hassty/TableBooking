using AutoMapper;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Models;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly GetAllRestaurants _getAllRestaurants;
        private readonly IMapper _mapper;

        public List<RestaurantModel> Restaurants { get; set; }

        public HomeViewModel(GetAllRestaurants getAllRestaurants, IMapper mapper)
        {
            _getAllRestaurants = getAllRestaurants;
            _mapper = mapper;

            LoadAllRestaurants();
        }

        public void LoadAllRestaurants()
        {
            Restaurants = _mapper.Map<List<RestaurantModel>>(_getAllRestaurants.GetRestaurants());
        }
    }
}