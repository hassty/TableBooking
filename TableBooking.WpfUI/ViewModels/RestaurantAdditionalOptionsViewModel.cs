using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class RestaurantAdditionalOptionsViewModel : ViewModelBase
    {
        private readonly INavigationService _goBackService;

        public string[] Days => DateTimeFormatInfo.InvariantInfo.DayNames;
        public ICommand GoBackCommand { get; }

        public RestaurantAdditionalOptionsViewModel(INavigationService goBackService)
        {
            _goBackService = goBackService;

            GoBackCommand = new DelegateCommand(GoBack);
        }

        private void GoBack(object obj)
        {
            _goBackService.Navigate();
        }
    }
}