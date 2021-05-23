using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;

namespace WpfUI.Stores
{
    public class AccountStore
    {
        private AccountModel _currentAccount;

        public AccountModel CurrentAccount
        {
            get => _currentAccount;
            set
            {
                if (_currentAccount != value)
                {
                    _currentAccount = value;
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