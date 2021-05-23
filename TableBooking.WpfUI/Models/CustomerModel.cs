using Core.Contracts.Dto;

namespace WpfUI.Models
{
    public class CustomerModel : UserModel, ICustomerDto
    {
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
    }
}