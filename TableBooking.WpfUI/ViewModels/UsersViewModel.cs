using AutoMapper;
using Core.Contracts.DataAccess;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TableBooking.Commands;
using TableBooking.Models;
using TableBooking.ViewModels;

namespace WpfUI.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private DelegateCommand loadAllUsersCommand;

        public ICommand LoadAllUsersCommand => loadAllUsersCommand ??= new DelegateCommand(LoadAllUsers);

        public ObservableCollection<CustomerModel> Users { get; set; }

        public UsersViewModel(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        private void LoadAllUsers(object obj)
        {
            Users = new ObservableCollection<CustomerModel>(_customerRepository.GetAll().Select(u => _mapper.Map<CustomerModel>(u)));
            OnPropertyChanged(nameof(Users));
        }
    }
}