using Core.Contracts.Dto;

namespace WpfUI.Models
{
    public class UserModel : ModelBase, IUserDto
    {
        private string _password;
        private string _username;

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
    }
}