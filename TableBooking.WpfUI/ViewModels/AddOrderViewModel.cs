using AutoMapper;
using Core.UseCases;
using System.Collections.Generic;
using WpfUI.Models;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AddOrderViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly RestaurantInteractor _restaurantInteractor;
        private RestaurantModel _restaurant { get; set; }
        public string Address { get => _restaurant.Address; }
        public List<int> Capacities { get; private set; }
        public string City { get => _restaurant.City; }
        public string Name { get => _restaurant.Name; }

        public AddOrderViewModel(
            CurrentRestaurantStore restaurantStore,
            RestaurantInteractor restaurantInteractor,
            IMapper mapper
        )
        {
            _restaurant = restaurantStore.CurrentRestaurant;
            _restaurantInteractor = restaurantInteractor;
            _mapper = mapper;

            GetCapacities();
        }

        public void GetCapacities()
        {
            Capacities = _restaurantInteractor.GetRestaurantTablesCapacities(_restaurant.Name, _restaurant.Address);
        }
    }
}