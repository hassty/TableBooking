using System;
using System.Collections.ObjectModel;

namespace WpfUI.Models
{
    public class RestaurantModel : ModelBase
    {
        private string _address;
        private string _city;
        private DateTime _latestOrderDate;
        private string _name;
        private TimeSpan _openedFrom;
        private TimeSpan _openedTill;

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

        public DateTime LatestOrderDate
        {
            get => _latestOrderDate;
            set
            {
                if (_latestOrderDate != value)
                {
                    _latestOrderDate = value;
                    OnPropertyChanged(nameof(LatestOrderDate));
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

        public ObservableCollection<DateTime> OffDates { get; set; }

        public TimeSpan OpenedFrom
        {
            get => _openedFrom;
            set
            {
                if (_openedFrom != value)
                {
                    _openedFrom = value;
                    OnPropertyChanged(nameof(OpenedFrom));
                }
            }
        }

        public TimeSpan OpenedTill
        {
            get => _openedTill;
            set
            {
                if (_openedTill != value)
                {
                    _openedTill = value;
                    OnPropertyChanged(nameof(OpenedTill));
                }
            }
        }

        public RestaurantModel()
        {
            OffDates = new ObservableCollection<DateTime>();
        }
    }
}