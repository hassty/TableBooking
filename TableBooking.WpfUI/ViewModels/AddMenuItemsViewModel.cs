using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class AddMenuItemsViewModel : ViewModelBase
    {
        private readonly INavigationService _goBackService;
        private MenuItemEntity _menuItem;
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

        public AddMenuItemsViewModel(INavigationService goBackService)
        {
            _goBackService = goBackService;

            _menuItem = new MenuItemEntity();

            GoBackCommand = new DelegateCommand(GoBack);
        }

        private void GoBack(object obj)
        {
            _goBackService.Navigate();
        }
    }
}