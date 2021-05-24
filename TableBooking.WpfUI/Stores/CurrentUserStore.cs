using System;
using WpfUI.Models;

namespace WpfUI.Stores
{
    public class CurrentUserStore
    {
        private UserModel _currentUser;

        public UserModel CurrentUser
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

        public bool IsLoggedIn => CurrentUser != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}