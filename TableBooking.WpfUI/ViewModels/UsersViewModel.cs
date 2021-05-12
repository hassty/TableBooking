using AutoMapper;
using Core.Contracts;
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
        private readonly IUserRepository _userRepository;
        private DelegateCommand loadAllUsersCommand;

        public ICommand LoadAllUsersCommand => loadAllUsersCommand ??= new DelegateCommand(LoadAllUsers);

        public ObservableCollection<UserModel> Users { get; set; }

        public UsersViewModel(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private void LoadAllUsers(object obj)
        {
            Users = new ObservableCollection<UserModel>(_userRepository.GetAll().Select(u => _mapper.Map<UserModel>(u)));
            OnPropertyChanged(nameof(Users));
        }
    }
}