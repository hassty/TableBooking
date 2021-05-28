using Core.Entities;
using Core.UseCases;
using System.Collections.Generic;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class RestaurantsViewModel : ViewModelBase
    {
        private readonly INavigationService _addRestaurantNavigator;
        private readonly GetRestaurants _getRestaurants;
        private RestaurantEntity _selectedRestaurant;

        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand InsertCommand { get; }
        public ICommand RemoveCommand { get; }

        public List<RestaurantEntity> Restaurants { get; set; }

        public RestaurantEntity SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                if (_selectedRestaurant != value)
                {
                    _selectedRestaurant = value;
                    OnPropertyChanged(nameof(SelectedRestaurant));
                }
            }
        }

        public RestaurantsViewModel(
            GetRestaurants getRestaurants,
            INavigationService goBackNavigator,
            INavigationService addRestaurantNavigator
        )
        {
            _getRestaurants = getRestaurants;
            _addRestaurantNavigator = addRestaurantNavigator;

            GoBackCommand = new DelegateCommand(_ => goBackNavigator.Navigate());
            InsertCommand = new DelegateCommand(_ => _addRestaurantNavigator.Navigate());
            RemoveCommand = new DelegateCommand(_ => { }, CanChangeRestaurant);
            EditCommand = new DelegateCommand(_ => { }, CanChangeRestaurant);

            LoadRestaurants();
        }

        private bool CanChangeRestaurant(object obj)
        {
            return _selectedRestaurant != null && Restaurants.Count != 0;
        }

        private void LoadRestaurants()
        {
            Restaurants = _getRestaurants.GetAllRestaurants();
            OnPropertyChanged(nameof(Restaurants));
        }
    }
}