using AutoMapper;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace WpfUI.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly LoginUser _loginUser;
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;
        private readonly LoginViewModel _viewModel;

        public LoginCommand(
            LoginViewModel viewModel,
            CurrentUserStore accountStore,
            INavigationService navigationService,
            IMapper mapper,
            LoginUser loginUser
        )
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
            _mapper = mapper;
            _loginUser = loginUser;
        }

        public override void Execute(object parameter)
        {
            try
            {
                var loggedInUser = _loginUser.Login(_viewModel.Username, _viewModel.Password);
                _accountStore.CurrentUser = _mapper.Map<UserModel>(loggedInUser);
                _navigationService.Navigate();
            }
            catch (InvalidCredentialsException)
            {
                throw;
            }
        }
    }
}