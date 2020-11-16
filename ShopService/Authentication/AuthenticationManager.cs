﻿using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;

namespace ShopService.Exceptions.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private IUserRepository _UserRepository { get; }

        public AuthenticationManager(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public bool CheckUserCredentials(string login, string password)
        {
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

        public void RegisterUser(User user)
        {
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
    }
}