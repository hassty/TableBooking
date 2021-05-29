using Core.Entities;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AddMenuItemsViewModel : ViewModelBase
    {
        private readonly AddMenuItem _addMenuItem;
        private readonly INavigationService _goBackService;
        private readonly CurrentRestaurantStore _restaurantStore;
        private MenuItemEntity _menuItem;
        public ICommand AddCommand { get; }
        public ICommand GoBackCommand { get; }

        public string Name
        {
            get => _menuItem.Name;
            set
            {
                if (_menuItem.Name != value)
                {
                    _menuItem.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public decimal Price
        {
            get => _menuItem.Price;
            set
            {
                if (_menuItem.Price != value)
                {
                    _menuItem.Price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public AddMenuItemsViewModel(
            CurrentRestaurantStore restaurantStore,
            AddMenuItem addMenuItem,
            INavigationService goBackService
        )
        {
            _restaurantStore = restaurantStore;
            _addMenuItem = addMenuItem;
            _goBackService = goBackService;

            _menuItem = new MenuItemEntity();

            GoBackCommand = new DelegateCommand(GoBack);
            AddCommand = new DelegateCommand(Add, CanAdd);
        }

        private void Add(object obj)
        {
            try
            {
                _addMenuItem.Add(_restaurantStore.CurrentRestaurant, _menuItem);
                _goBackService.Navigate();
            }
            catch (Exception)
            {
                MessageBox.Show("Item already exists");
            }
        }

        private bool CanAdd(object arg)
        {
            return !String.IsNullOrWhiteSpace(Name) && Price >= 0;
        }

        private void GoBack(object obj)
        {
            _goBackService.Navigate();
        }
    }
}