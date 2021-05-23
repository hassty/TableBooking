using AutoMapper;
using Core.Entities;
using Core.UseCases;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Models;

namespace WpfUI.ViewModels
{
    public class RestaurantsViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly RestaurantsInteractor _restaurantsInteractor;
        private DelegateCommand _addCommand;
        private DelegateCommand _removeCommand;
        private RestaurantModel _selectedRestaurant;

        public ICommand AddCommand
        {
            get => _addCommand ??= new DelegateCommand(AddRestaurant);
        }

        public ICommand RemoveCommand
        {
            get => _removeCommand ??= new DelegateCommand(RemoveRestaurant, _ => Restaurants.Count > 0);
        }

        public ObservableCollection<RestaurantModel> Restaurants { get; set; }

        public RestaurantModel SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                _selectedRestaurant = value;
                OnPropertyChanged(nameof(SelectedRestaurant));
            }
        }

        public RestaurantsViewModel(RestaurantsInteractor restaurantsInteractor, IMapper mapper)
        {
            _restaurantsInteractor = restaurantsInteractor;
            _mapper = mapper;
            LoadAllRestaurants();
        }

        private void AddRestaurant(object obj)
        {
            var newRestaurant = new RestaurantModel
            {
                Name = _selectedRestaurant.Name,
                City = _selectedRestaurant.City,
                Address = _selectedRestaurant.Address,
                OpenedFrom = _selectedRestaurant.OpenedFrom,
                OpenedTill = _selectedRestaurant.OpenedTill
            };
            _restaurantsInteractor.AddRestaurant(_mapper.Map<RestaurantEntity>(newRestaurant));
            Restaurants.Add(newRestaurant);
            SelectedRestaurant = newRestaurant;
        }

        private void LoadAllRestaurants()
        {
            Restaurants = new ObservableCollection<RestaurantModel>(_restaurantsInteractor.GetAllRestaurants().Select(r => _mapper.Map<RestaurantModel>(r)));
            OnPropertyChanged(nameof(Restaurants));
        }

        private void RemoveRestaurant(object obj)
        {
            _restaurantsInteractor.RemoveRestaurant(_mapper.Map<RestaurantEntity>(_selectedRestaurant));
            Restaurants.Remove(_selectedRestaurant);
        }
    }
}