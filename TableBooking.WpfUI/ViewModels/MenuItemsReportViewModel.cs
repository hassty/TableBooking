using Core.Entities;
using Core.UseCases;
using System.Collections.Generic;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class MenuItemsReportViewModel : ViewModelBase
    {
        private readonly GetRestaurantMenuItems _getMenuItems;
        private readonly INavigationService _goBackNavigator;
        private readonly CurrentRestaurantStore _restaurantStore;

        public ICommand GoBackCommand { get; }
        public List<MenuItemEntity> MenuItems { get; set; }

        public MenuItemsReportViewModel(
            GetRestaurantMenuItems getMenuItems,
            CurrentRestaurantStore restaurantStore,
            INavigationService goBackNavigator
        )
        {
            _getMenuItems = getMenuItems;
            _restaurantStore = restaurantStore;
            _goBackNavigator = goBackNavigator;

            GoBackCommand = new DelegateCommand(_ => goBackNavigator.Navigate());

            LoadItems();
        }

        private void LoadItems()
        {
            MenuItems = _getMenuItems.GetMenuItems(_restaurantStore.CurrentRestaurant);
            OnPropertyChanged(nameof(MenuItems));
        }
    }
}