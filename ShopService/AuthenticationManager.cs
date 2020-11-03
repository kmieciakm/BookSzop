using DatabaseManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService
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
    }
}
