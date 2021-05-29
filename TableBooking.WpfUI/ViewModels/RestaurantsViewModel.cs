using Core.Entities;
using Core.Exceptions;
using Core.UseCases;
using System;
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
        private readonly INavigationService _menuItemsNavigator;
        private readonly RemoveRestaurant _removeRestaurant;
        private readonly CurrentRestaurantStore _restaurantStore;
        private readonly INavigationService _updateRestaurantNavigator;
        private RestaurantEntity _selectedRestaurant;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand GoToMenuItemsCommand { get; }
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
            INavigationService menuItemsNavigator,
            INavigationService addRestaurantNavigator,
            INavigationService updateRestaurantNavigator
        )
        {
            _restaurantStore = restaurantStore;
            _getRestaurants = getRestaurants;
            _removeRestaurant = removeRestaurant;
            _menuItemsNavigator = menuItemsNavigator;
            _addRestaurantNavigator = addRestaurantNavigator;
            _updateRestaurantNavigator = updateRestaurantNavigator;

            GoBackCommand = new DelegateCommand(_ => goBackNavigator.Navigate());
            AddCommand = new DelegateCommand(_ => _addRestaurantNavigator.Navigate());

            GoToMenuItemsCommand = new DelegateCommand(EditMenu, CanChangeRestaurant);
            RemoveCommand = new DelegateCommand(RemoveRestaurant, CanChangeRestaurant);
            EditCommand = new DelegateCommand(EditRestaurant, CanChangeRestaurant);

            LoadRestaurants();
        }

        private bool CanChangeRestaurant(object obj)
        {
            return _selectedRestaurant != null && Restaurants.Count != 0;
        }

        private void EditMenu(object obj)
        {
            _restaurantStore.CurrentRestaurant = _selectedRestaurant;
            _menuItemsNavigator.Navigate();
        }

        private void EditRestaurant(object obj)
        {
            _restaurantStore.CurrentRestaurant = _selectedRestaurant;
            _updateRestaurantNavigator.Navigate();
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
            catch (Exception ex) when (
                ex is RestaurantOrdersException
                || ex is ItemAlreadyExistsException
            )
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}