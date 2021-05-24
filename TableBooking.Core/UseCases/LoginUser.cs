using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Entities.Users;
using Core.Exceptions;

namespace Core.UseCases
{
    public class LoginUser
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;

        public LoginUser(
            ICustomerRepository customerRepository,
            IAdminRepository adminRepository,
            IPasswordProtectionStrategy passwordProtectionStrategy
        )
        {
            _customerRepository = customerRepository;
            _adminRepository = adminRepository;
            _passwordProtectionStrategy = passwordProtectionStrategy;
        }

        /// <exception cref="InvalidCredentialsException"></exception>
        public UserEntity Login(string username, string password)
        {
            if (_customerRepository.ContainsUserWithUsername(username))
            {
                var customer = _customerRepository.GetUserWithUsername(username);
                var saltedPassword = $"{customer.Salt}{password}";
                var passwordHash = _passwordProtectionStrategy.GetProtectedPassword(saltedPassword);
                if (customer.PasswordHash == passwordHash)
                {
                    return customer;
                }
            }
            if (_adminRepository.ContainsUserWithUsername(username))
            {
                var admin = _adminRepository.GetUserWithUsername(username);
                var saltedPassword = $"{admin.Salt}{password}";
                var passwordHash = _passwordProtectionStrategy.GetProtectedPassword(saltedPassword);
                if (admin.PasswordHash == passwordHash)
                {
                    return admin;
                }
            }

            throw new InvalidCredentialsException("Invalid credentials");
        }
    }
}
