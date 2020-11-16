using DatabaseManager.Models;
using System.Collections.Generic;

namespace ShopService.StoreManagement
{
    public interface IStoreManagementService
    {
        List<BookBundle> GetAllBookBundles();
        List<Book> GetAllBooks();
        void RegisterBook(Book book);
        void RegisterBookBundle(BookBundle bookBundle);
        void RemoveBook(int bookId);
        void RemoveBookBundle(int bookBundleId);
    }
}