using DatabaseManager.Models;
using System.Collections.Generic;

namespace ShopService
{
    public interface IShelfService
    {
        List<Book> GetUserBooks(int userId);
    }
}