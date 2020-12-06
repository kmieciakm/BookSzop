using DatabaseManager.Models;
using DatabaseManager.Repository.Contracts;
using ShopService.UserServ;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.UserServ
{
    class UserService : IUserService
    {
        private IUserRepository _UserRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public List<Book> GetBooksOfUser(int id)
        {
            var user = _UserRepository.FindById(id);

            List<Book> ownedBooks = user.OwnedBooks;
            return ownedBooks;
        }

        public string GetUserName(int userId)
        {
            return _UserRepository.FindById(userId).FullName;
        }
    }
}
