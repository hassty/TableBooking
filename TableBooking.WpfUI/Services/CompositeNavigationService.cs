using System;
using System.Collections.Generic;
using System.Text;

namespace WpfUI.Services
{
    public class CompositeNavigationService : INavigationService
    {
        private readonly IEnumerable<INavigationService> _navigationServices;

        public CompositeNavigationService(params INavigationService[] navigationServices)
        {
            _navigationServices = navigationServices;
        }

        public void Navigate()
        {
            foreach (var navigationService in _navigationServices)
            {
                navigationService.Navigate();
            }
        }
    }
}