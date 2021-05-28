using Core.Entities;
using Core.Exceptions;
using Core.UseCases;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class RestaurantsViewModel : ViewModelBase
    {
        private readonly INavigationService _addRestaurantNavigator;
        private readonly GetRestaurants _getRestaurants;
        private readonly RemoveRestaurant _removeRestaurant;
        private readonly CurrentRestaurantStore _restaurantStore;
        private RestaurantEntity _selectedRestaurant;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
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
            CurrentRestaurantStore restaurantStore,
            GetRestaurants getRestaurants,
            RemoveRestaurant removeRestaurant,
            INavigationService goBackNavigator,
            INavigationService addRestaurantNavigator
        )
        {
            _restaurantStore = restaurantStore;
            _getRestaurants = getRestaurants;
            _removeRestaurant = removeRestaurant;
            _addRestaurantNavigator = addRestaurantNavigator;

            GoBackCommand = new DelegateCommand(_ => goBackNavigator.Navigate());

            AddCommand = new DelegateCommand(AddRestaurant);
            RemoveCommand = new DelegateCommand(RemoveRestaurant, CanChangeRestaurant);
            EditCommand = new DelegateCommand(EditRestaurant, CanChangeRestaurant);

            LoadRestaurants();
        }

        private void AddRestaurant(object obj)
        {
            _restaurantStore.CurrentRestaurant = null;
            _addRestaurantNavigator.Navigate();
        }

        private bool CanChangeRestaurant(object obj)
        {
            return _selectedRestaurant != null && Restaurants.Count != 0;
        }

        private void EditRestaurant(object obj)
        {
            _restaurantStore.CurrentRestaurant = _selectedRestaurant;
            _addRestaurantNavigator.Navigate();
        }

        private void LoadRestaurants()
        {
            Restaurants = _getRestaurants.GetAllRestaurants();
            OnPropertyChanged(nameof(Restaurants));
        }

        private void RemoveRestaurant(object obj)
        {
            try
            {
                _removeRestaurant.Remove(_selectedRestaurant);
                LoadRestaurants();
            }
            catch (ItemNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}