using DatabaseManager.Models;
using System.Collections.Generic;

namespace ShopService.UserServ
{
    public interface IUserService
    {
        List<Book> GetBooksOfUser(int id);
        string GetUserName(int userId);
        int GetOwnedBookAmount(int userId, int bookId);
    }
}