using ShopService.Models.BookModel;
using System.Collections.Generic;

namespace ShopService.UserServ
{
    public interface IUserService
    {
        IEnumerable<IBook> GetBooksOfUser(int id);
        string GetUserName(int userId);
        int GetOwnedBookAmount(int userId, int bookId);
    }
}