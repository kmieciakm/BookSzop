using DatabaseManager.Models;
using DatabaseManager.Models.Enums;
using DatabaseManager.Repository.Contracts;
using ShopService.Models;
using ShopService.Models.BookOrderModel;
using ShopService.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopService.Purchase
{
    class PurchaseService : IPurchaseService
    {
        private IEventsRepository _EventsRepository { get; }
        private IUserRepository _UserRepository { get; }
        private IBookBundleRepositiory _BookBundleRepository { get; }

        public PurchaseService(IEventsRepository eventsRepository, IUserRepository userRepository,
            IBookBundleRepositiory bookBundleRepositiory)
        {
            _EventsRepository = eventsRepository;
            _UserRepository = userRepository;
            _BookBundleRepository = bookBundleRepositiory;
        }

        public IEnumerable<IEvent> GetAllPurchases()
        {
            return _EventsRepository
                .FindAll()
                .Where(@event => @event.EventType == EventType.Purchase)
                .Select(@event => Mapper.DatabaseEventToServiceEvent(@event))
                .ToList<IEvent>();
        }

        public IEnumerable<IEvent> GetAllRefunds()
        {
            return _EventsRepository
                .FindAll()
                .Where(@event => @event.EventType == EventType.Refund)
                .Select(@event => Mapper.DatabaseEventToServiceEvent(@event))
                .ToList<IEvent>();
        }

        public IEnumerable<IEvent> GetUserPurchases(int userId)
        {
            return _EventsRepository
               .FindAll()
               .Where(@event =>
                    @event.EventType == EventType.Purchase &&
                    @event.UserId == userId)
               .Select(@event => Mapper.DatabaseEventToServiceEvent(@event))
               .ToList<IEvent>();
        }

        public IEnumerable<IEvent> GetUserRefunds(int userId)
        {
            return _EventsRepository
               .FindAll()
               .Where(@event =>
                    @event.EventType == EventType.Refund &&
                    @event.UserId == userId)
               .Select(@event => Mapper.DatabaseEventToServiceEvent(@event))
               .ToList<IEvent>();
        }

        public void MakePurchase(int userId, IEnumerable<IBookOrderCreate> booksToOrder)
        {
            if (!_UserRepository.Exists(userId))
            {
                throw new PurchaseException($"Purchase unavailable, wrong {nameof(userId)} {userId}");
            };

            foreach (var bookToOrder in booksToOrder)
            {
                var bookBundle = _BookBundleRepository.FindById(bookToOrder.BookBundleId);
                if (bookBundle == null)
                {
                    throw new PurchaseException($"Purchase unavailable, wrong {nameof(bookToOrder.BookBundleId)} {bookToOrder.BookBundleId}");
                }
                if (bookToOrder.Quantity > bookBundle.Quantity)
                {
                    throw new PurchaseException($"Purchase unavailable, too many books requested, max {bookBundle.Quantity}");
                }
            }

            List<BookOrder> orderedBooks = new List<BookOrder>();
            foreach (var bookToOrder in booksToOrder)
            {
                orderedBooks.Add(
                    new BookOrder()
                    {
                        BookBundleId = bookToOrder.BookBundleId,
                        Quantity = bookToOrder.Quantity
                    }
                );
            }

            var purchase = new DatabaseManager.Models.Event()
            {
                EventType = EventType.Purchase,
                PlacedDate = DateTime.UtcNow,
                UserId = userId,
                OrderedBooks = orderedBooks
            };

            var orderPlacedResult = _EventsRepository.Create(purchase);
            if (!orderPlacedResult)
            {
                throw new PurchaseException($"Purchase unavailable, an unexpected error occurred.");
            }

            var bundles = new List<BookBundle>();
            foreach (var bookToOrder in booksToOrder)
            {
                var bundle = _BookBundleRepository.FindById(bookToOrder.BookBundleId);
                bundle.Quantity -= bookToOrder.Quantity;
                bundles.Add(bundle);
            }

            var bundlesUpdateResult = _BookBundleRepository.UpdateBundles(bundles);
            if (!bundlesUpdateResult)
            {
                _EventsRepository.Delete(purchase);
                throw new PurchaseException($"Purchase unavailable, an unexpected error occurred.");
            }
        }

        public void PlaceRefund(int userId, int eventId)
        {
            var user = _UserRepository.FindById(userId);
            if (user == null)
            {
                throw new PurchaseException($"Refund unavailable, wrong {nameof(userId)} {userId}");
            };

            var purchase = user.Purchases?.FirstOrDefault(order => order.Id == eventId);
            if (purchase == null)
            {
                throw new PurchaseException($"Refund unavailable, user does not have order of id {eventId}");
            };

            var refund = new DatabaseManager.Models.Event()
            {
                EventType = EventType.Refund,
                PlacedDate = DateTime.UtcNow,
                UserId = userId,
                OrderedBooks = purchase.OrderedBooks
            };

            var refundPlacedResult = _EventsRepository.Create(refund);

            if (!refundPlacedResult)
            {
                throw new PurchaseException($"Refund unavailable, an unexpected error occurred.");
            }

            var booksRefunded = purchase.OrderedBooks;
            var bundles = new List<BookBundle>();
            foreach (var bookOrder in booksRefunded)
            {
                var bundle = _BookBundleRepository.FindById(bookOrder.BookBundleId);
                bundle.Quantity += bookOrder.Quantity;
                bundles.Add(bundle);
            }

            var bundlesUpdateResult = _BookBundleRepository.UpdateBundles(bundles);
            if (!bundlesUpdateResult)
            {
                _EventsRepository.Delete(refund);
                throw new PurchaseException($"Refund unavailable, an unexpected error occurred.");
            }
        }
    }
}
