using Core.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;

namespace WpfUI.Stores
{
    public class AccountStore
    {
        private UserModel _currentUser;

        public UserModel CurrentAccount
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    CurrentAccountChanged?.Invoke();
                }
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }
    }
}