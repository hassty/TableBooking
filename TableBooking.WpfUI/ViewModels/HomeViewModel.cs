using Core.Entities;
using Core.UseCases;
using System.Collections.Generic;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService _addOrderNavigationService;
        private readonly GetRestaurants _getAllRestaurants;
        private readonly CurrentRestaurantStore _restaurantStore;
        private readonly CurrentUserStore _userStore;
        private RestaurantEntity _selectedRestaurant;
        public ICommand AddOrderCommand { get; }
        public bool IsLoggedIn => _userStore.IsLoggedIn;
        public List<RestaurantEntity> Restaurants { get; set; }

        public RestaurantEntity SelectedRestaurant

        {
            get => _selectedRestaurant;
            set
            {
                if (_selectedRestaurant != value)
                {
                    _selectedRestaurant = value;
                    _restaurantStore.CurrentRestaurant = value;
                    OnPropertyChanged(nameof(SelectedRestaurant));
                }
            }
        }

        public HomeViewModel(
            CurrentRestaurantStore restaurantStore,
            CurrentUserStore userStore,
            GetRestaurants getAllRestaurants,
            INavigationService addOrderNavigationService
        )
        {
            _restaurantStore = restaurantStore;
            _userStore = userStore;
            _getAllRestaurants = getAllRestaurants;
            _addOrderNavigationService = addOrderNavigationService;

            AddOrderCommand = new DelegateCommand(AddOrder, CanAddOrder);

            LoadAllRestaurants();
        }

        private void AddOrder(object parameter)
        {
            _addOrderNavigationService.Navigate();
        }

        private bool CanAddOrder(object parameter)
        {
            return _selectedRestaurant != null;
        }

        public void LoadAllRestaurants()
        {
            Restaurants = _getAllRestaurants.GetAllRestaurants();
        }
    }
}