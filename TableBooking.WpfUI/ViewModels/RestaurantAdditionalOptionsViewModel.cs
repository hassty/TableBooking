using Core.Entities;
using System;
using System.Globalization;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class RestaurantAdditionalOptionsViewModel : ViewModelBase
    {
        private readonly INavigationService _goBackService;
        private readonly RestaurantOrderOptionsEntity _options;
        private readonly CurrentRestaurantStore _restaurantStore;
        public string[] Days => DateTimeFormatInfo.InvariantInfo.DayNames;

        public ICommand GoBackCommand { get; }
        public ICommand SaveCommand { get; }

        #region Bindable Properties

        public int LatestOrderDate
        {
            get => _options.LatestOrderDate;
            set
            {
                if (_options.LatestOrderDate != value)
                {
                    _options.LatestOrderDate = value;
                    OnPropertyChanged(nameof(LatestOrderDate));
                }
            }
        }

        public int LongestHours
        {
            get => _options.LongestReservationDuration.Hours;
            set
            {
                if (_options.LongestReservationDuration.Hours != value)
                {
                    _options.LongestReservationDuration = new TimeSpan(value, LongestMinutes, 0);
                    OnPropertyChanged(nameof(LongestHours));
                }
            }
        }

        public int LongestMinutes
        {
            get => _options.LongestReservationDuration.Minutes;
            set
            {
                if (_options.LongestReservationDuration.Minutes != value)
                {
                    _options.LongestReservationDuration = new TimeSpan(LongestHours, value, 0);
                    OnPropertyChanged(nameof(LongestMinutes));
                }
            }
        }

        public int MaxPartySize
        {
            get => _options.MaxPartySize;
            set
            {
                if (_options.MaxPartySize != value)
                {
                    _options.MaxPartySize = value;
                    OnPropertyChanged(nameof(MaxPartySize));
                }
            }
        }

        public int ShortestHours
        {
            get => _options.ShortestReservationDuration.Hours;
            set
            {
                if (_options.ShortestReservationDuration.Hours != value)
                {
                    _options.ShortestReservationDuration = new TimeSpan(value, ShortestMinutes, 0);
                    OnPropertyChanged(nameof(ShortestHours));
                }
            }
        }

        public int ShortestMinutes
        {
            get => _options.ShortestReservationDuration.Minutes;
            set
            {
                if (_options.ShortestReservationDuration.Minutes != value)
                {
                    _options.ShortestReservationDuration = new TimeSpan(ShortestHours, value, 0);
                    OnPropertyChanged(nameof(ShortestMinutes));
                }
            }
        }

        #endregion Bindable Properties

        public RestaurantAdditionalOptionsViewModel(
            CurrentRestaurantStore restaurantStore,
            INavigationService goBackService
        )
        {
            _restaurantStore = restaurantStore;
            _goBackService = goBackService;

            if (restaurantStore.CurrentRestaurant != null)
            {
                _options = restaurantStore.CurrentRestaurant.OrderOptions;
            }
            else
            {
                _options = new RestaurantOrderOptionsEntity();
            }

            GoBackCommand = new DelegateCommand(GoBack);
            SaveCommand = new DelegateCommand(Save);
        }

        private void GoBack(object obj)
        {
            _goBackService.Navigate();
        }

        private void Save(object obj)
        {
            _goBackService.Navigate();
        }
    }
}