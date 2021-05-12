using Core.Contracts;
using Core.Entities.Users;
using System;

namespace TableBooking.ConsoleUI
{
    public class Application : IApplication
    {
        private IRestaurantRepository _restaurantRepository;
        private IUserRepository _userRepository;

        public Application(IUserRepository userRepository, IRestaurantRepository restaurantRepository)
        {
            _userRepository = userRepository;
            _restaurantRepository = restaurantRepository;
        }

        public void Run()
        {
            var admin = new AdminEntity { Username = "gp", PasswordHash = "1488" };
            var restaurants = _restaurantRepository.GetAll();
            foreach (var item in restaurants)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}