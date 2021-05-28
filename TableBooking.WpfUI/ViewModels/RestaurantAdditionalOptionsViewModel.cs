using Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class RestaurantAdditionalOptionsViewModel : ViewModelBase
    {
        private readonly INavigationService _goBackService;
        private readonly RestaurantOrderOptionsEntity _optionsEntity;
        private readonly CurrentRestaurantStore _restaurantStore;
        public string[] Days => DateTimeFormatInfo.InvariantInfo.DayNames;
        public ICommand GoBackCommand { get; }

        public int LatestOrderDate
        {
            get => _optionsEntity.LatestOrderDate;
            set
            {
                if (_optionsEntity.LatestOrderDate != value)
                {
                    _optionsEntity.LatestOrderDate = value;
                    OnPropertyChanged(nameof(LatestOrderDate));
                }
            }
        }

        public RestaurantAdditionalOptionsViewModel(
            CurrentRestaurantStore restaurantStore,
            INavigationService goBackService
        )
        {
            _restaurantStore = restaurantStore;
            _goBackService = goBackService;

            if (restaurantStore.CurrentRestaurant != null)
            {
                _optionsEntity = restaurantStore.CurrentRestaurant.OrderOptions;
            }
            else
            {
                _optionsEntity = new RestaurantOrderOptionsEntity();
            }

            GoBackCommand = new DelegateCommand(GoBack);
        }

        private void GoBack(object obj)
        {
            _goBackService.Navigate();
        }
    }
}