using ShopService.Models.BookModel;
using ShopService.Models.BookBundleModel;
using System.Collections.Generic;

namespace ShopService.StoreManagement
{
    public interface IStoreManagementService
    {
        IEnumerable<IBookBundle> GetAllBookBundles();
        IEnumerable<IBook> GetAllBooks();
        void RegisterBook(IBook book);
        void RegisterBookBundle(IBookBundle bookBundle);
        void RemoveBook(int bookId);
        void RemoveBookBundle(int bookBundleId);
    }
}