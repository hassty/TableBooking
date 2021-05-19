using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Contracts.Dto;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class RegisterAdmin
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;

        public RegisterAdmin(
            IAdminRepository adminRepository,
            IPasswordProtectionStrategy passwordProtectionStrategy
        )
        {
            _adminRepository = adminRepository;
            _passwordProtectionStrategy = passwordProtectionStrategy;
        }

        /// <exception cref="UserAlreadyExistsException"></exception>
        public void Register(IAdminDto admin)
        {
            if (_adminRepository.ContainsUserWithUsername(admin.Username))
            {
                throw new UserAlreadyExistsException("User with this username already exists");
            }

            var newAdmin = admin.ToEntity();
            (newAdmin.Salt, newAdmin.PasswordHash) = _passwordProtectionStrategy.HashAndSaltPassword(admin.Password);

            _adminRepository.Add(newAdmin);
            _adminRepository.SaveChanges();
        }
    }
}
