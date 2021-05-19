using Core.Contracts.DataAccess;
using Core.Entities.Users;
using Core.Exceptions;
using System;

namespace Core.UseCases
{
    public class RegisterCustomer
    {
        private readonly ICustomerRepository _customerRespository;
        private readonly Func<string, string> _hashingAlgorithm;

        public RegisterCustomer(ICustomerRepository customerRespository, Func<string, string> hashingAlgorithm)
        {
            _customerRespository = customerRespository;
            _hashingAlgorithm = hashingAlgorithm;
        }


        /// <exception cref="UserAlreadyExistsException"></exception>
        public void Execute(CustomerEntity newCustomer)
        {
            if (_customerRespository.ContainsCustomerWithUsername(newCustomer.Username))
            {
                throw new UserAlreadyExistsException("User with this username already exists");
            }
            newCustomer.PasswordHash = _hashingAlgorithm(newCustomer.PasswordHash);

            _customerRespository.Add(newCustomer);
        }
    }
}
