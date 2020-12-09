using DatabaseManager.Repository.Contracts;
using ShopService.Models;
using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<IBook> GetBooksOfUser(int id)
        {
            var user = _UserRepository.FindById(id);

            return user?.OwnedBooks
                ?.Select(bookData => Mapper.DatabaseBookToServiceBook(bookData))
                .ToList<IBook>();
        }

        public string GetUserName(int userId)
        {
            return _UserRepository.FindById(userId).FullName;
        }

        public int GetOwnedBookAmount(int userId, int bookId)
        {
            var user = _UserRepository.FindById(userId);

            var orders = user.Purchases?
                    .Select(purchase => purchase.OrderedBooks)
                    .Except(
                        user.Refunds.Select(refund => refund.OrderedBooks))
                    .SelectMany(orders => orders).ToList();

            return orders.Where(order => order.BookBundle.BookId == bookId).Sum(order => order.Quantity);
        }
    }
}
