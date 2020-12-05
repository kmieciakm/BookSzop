using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using ShopService.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;

namespace ShopService.Authentication
{
    class AuthenticationManager : IAuthenticationManager
    {
        private IUserRepository _UserRepository { get; }

        public AuthenticationManager(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public bool CheckUserCredentials(string login, string password)
        {
            if (login == null || password == null)
            {
                return false;
            }

            var user = _UserRepository.GetUserByLogin(login);
            if (user?.Password == password)
            {
                return true;
            }

            return false;
        }

        public bool CheckAdminAccess(int userId)
        {
            var user = _UserRepository.FindById(userId);
            return user.AdminPermission == true;
        }

        public void RegisterUser(IUserCreate userToCreate)
        {
            if (!userToCreate.ConfirmationPasswordCorrect())
            {
                throw new AuthenticationException($"Given passwords differ.");
            }

            var user = new User()
            {
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Login = userToCreate.Login,
                Password = userToCreate.Password,
                AdminPermission = false
            };

            if (!_UserRepository.IsLoginFree(user.Login))
            {
                throw new AuthenticationException($"User of login {user.Login} already exists.");
            }

            var userCreated = _UserRepository.Create(user);
            if (!userCreated)
            {
                throw new AuthenticationException("Somthing went wrong. Registration not succeeded.");
            }
        }

        public int? GetUserIdByLogin(string login)
        {
            return _UserRepository.GetUserByLogin(login)?.Id;
        }
    }
}
