using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateLoginCommand { get; }

        public HomeViewModel(INavigationService loginNavigationService)
        {
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        }
    }
}