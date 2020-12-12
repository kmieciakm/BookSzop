using ShopService.Models.BookModel;
using ShopService.Models.BookBundleModel;
using System.Collections.Generic;

namespace ShopService.StoreManagement
{
    public interface IStoreManagementService
    {
        IEnumerable<IBookBundle> GetAllBookBundles();
        IEnumerable<IBook> GetAllBooks();
        bool BookExists(int bookId);
        void RegisterBook(IBook book);
        void RegisterBookBundle(IBookBundle bookBundle);
        void UpdateBook(IBook book);
        void UpdateBookBundle(IBookBundle bundle);
        void RemoveBook(int bookId);
        void RemoveBookBundle(int bookBundleId);
    }
}