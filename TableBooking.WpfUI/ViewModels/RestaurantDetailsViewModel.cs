using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;

namespace WpfUI.ViewModels
{
    public class RestaurantDetailsViewModel : ViewModelBase
    {
        private string _address;
        private string _city;
        private int _hoursFrom;
        private int _hoursTill;
        private int _minutesFrom;
        private int _minutesTill;
        private string _name;
        private RestaurantEntity _restaurant;

        public ICommand AdditionalOptionsCommand { get; }
        public ICommand AddMenuItemsCommand { get; }
        public ICommand SaveCommand { get; }

        #region Bindable Properties

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public int HoursFrom
        {
            get => _hoursFrom;
            set
            {
                if (_hoursFrom != value)
                {
                    _hoursFrom = value;
                    OnPropertyChanged(nameof(HoursFrom));
                }
            }
        }

        public int HoursTill
        {
            get => _hoursTill;
            set
            {
                if (_hoursTill != value)
                {
                    _hoursTill = value;
                    OnPropertyChanged(nameof(HoursTill));
                }
            }
        }

        public int MinutesFrom
        {
            get => _minutesFrom;
            set
            {
                if (_minutesFrom != value)
                {
                    _minutesFrom = value;
                    OnPropertyChanged(nameof(MinutesFrom));
                }
            }
        }

        public int MinutesTill
        {
            get => _minutesTill;
            set
            {
                if (_minutesTill != value)
                {
                    _minutesTill = value;
                    OnPropertyChanged(nameof(MinutesTill));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        #endregion Bindable Properties

        public RestaurantDetailsViewModel()
        {
            _restaurant = new RestaurantEntity();
        }
    }
}