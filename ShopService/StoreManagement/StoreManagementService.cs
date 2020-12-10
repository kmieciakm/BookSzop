using DatabaseManager.Repository.Contracts;
using ShopService.Models;
using ShopService.Models.BookModel;
using ShopService.Models.BookBundleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopService.StoreManagement
{
    class StoreManagementService : IStoreManagementService
    {
        private IBookRepository _BookRepository { get; }
        private IBookBundleRepositiory _BookBundleRepository { get; }

        public StoreManagementService(IBookRepository bookRepository, IBookBundleRepositiory bookBundleRepositiory)
        {
            _BookRepository = bookRepository;
            _BookBundleRepository = bookBundleRepositiory;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _BookRepository
                .FindAll()
                .Where(book => book.IsAvailable)
                .Select(bookData => Mapper.DatabaseBookToServiceBook(bookData))
                .ToList<IBook>();
        }

        public IEnumerable<IBookBundle> GetAllBookBundles()
        {
            return _BookBundleRepository
                .FindAll()
                .Where(bundle => bundle.IsAvailable)
                .Select(bundle => Mapper.DatabaseBookBundleToServiceBookBundle(bundle))
                .ToList();
        }

        public void RegisterBook(IBook book)
        {
            if (_BookRepository.Exists(book.Id))
            {
                throw new StoreManagementException($"Unable to register {nameof(book)}, such {nameof(book.Id)} already exists");
            }

            var bookEntity = new DatabaseManager.Models.Book()
            {
                Title = book.Title,
                Author = book.Author
            };

            var registerBookResult = _BookRepository.Create(bookEntity);

            if (!registerBookResult)
            {
                throw new StoreManagementException($"Book registration impossible, an unexpected error occurred.");
            }
        }

        public void UpdateBook(IBook book)
        {
            var bookToUpdate = _BookRepository.FindById(book.Id);
            if (bookToUpdate == null)
            {
                throw new StoreManagementException($"Book update impossible, not such book of id {book.Id} exists.");
            }

            bookToUpdate.Author = book.Author;
            bookToUpdate.Title = book.Title;

            var updateResult = _BookRepository.Update(bookToUpdate);
            if (!updateResult)
            {
                throw new StoreManagementException($"Book impossible to update, unknown error occurred.");
            }
        }

        public void RemoveBook(int bookId)
        {
            var bookToDelete = _BookRepository
                .FindById(bookId);

            if (bookToDelete == null)
            {
                throw new StoreManagementException($"Unable to remove {nameof(bookToDelete)}, such {nameof(bookToDelete.Id)} doesn't exists");
            }

            var deletedBook = _BookRepository.SoftDelete(bookToDelete);

            // Delete all bundles that contains this book
            var bundlesToDelete = _BookBundleRepository
                .FindAll()
                .Where(bundle => bundle.BookId == bookId && bundle.IsAvailable)
                .ToList();

            bundlesToDelete.ForEach(bundle => _BookBundleRepository.SoftDelete(bundle));

            if (!deletedBook)
            {
                throw new StoreManagementException($"{nameof(bookToDelete)} impossible to delete, unknown error occurred");
            }
        }

        public void RegisterBookBundle(IBookBundle bookBundle)
        {
            if (_BookBundleRepository.Exists(bookBundle.Id))
            {
                throw new StoreManagementException($"Unable to register {nameof(bookBundle)}, such {nameof(bookBundle.Id)} already exists");
            }

            var bookBundleEntity = Mapper.ServiceBookBundleToDatabaseBookBundle(bookBundle);
            var registerBookBundleResult = _BookBundleRepository.Create(bookBundleEntity);

            if (!registerBookBundleResult)
            {
                throw new StoreManagementException($"{nameof(bookBundle)} registration impossible, an unexpected error occurred.");
            }
        }

        public void RemoveBookBundle(int bookBundleId)
        {
            var bookBundleToDelete = _BookBundleRepository
                .FindById(bookBundleId);

            if (bookBundleToDelete == null)
            {
                throw new StoreManagementException($"Unable to remove {nameof(bookBundleToDelete)}, such {nameof(bookBundleToDelete.Id)} doesn't exists");
            }

            var deletedBookBundle = _BookBundleRepository.SoftDelete(bookBundleToDelete);

            if (!deletedBookBundle)
            {
                throw new StoreManagementException($"{nameof(bookBundleToDelete)} impossible to delete, unknown error occurred");
            }
        }
    }
}
