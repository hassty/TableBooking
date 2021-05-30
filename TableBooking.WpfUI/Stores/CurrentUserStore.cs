using Core.Entities.Users;
using System;

namespace WpfUI.Stores
{
    public class CurrentUserStore
    {
        private UserEntity _currentUser;

        public UserEntity CurrentUser
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

        public bool IsAdmin => CurrentUser is AdminEntity;
        public bool IsCustomer => CurrentUser is CustomerEntity;
        public bool IsLoggedIn => CurrentUser != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}