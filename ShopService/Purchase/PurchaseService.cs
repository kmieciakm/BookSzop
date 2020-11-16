using DatabaseManager.Models;
using DatabaseManager.Models.Enums;
using DatabaseManager.Repository.Contracts;
using ShopService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopService.Purchase
{
    public class PurchaseService : IPurchaseService
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

        public List<Event> GetAllOrders()
        {
            return _EventsRepository
                .FindAll()
                .Where(@event => @event.EventType == EventType.Order)
                .ToList();
        }

        public List<Event> GetAllRefunds()
        {
            return _EventsRepository
                .FindAll()
                .Where(@event => @event.EventType == EventType.Refund)
                .ToList();
        }

        public List<Event> GetOrdersOfUser(int userId)
        {
            return _EventsRepository
               .FindAll()
               .Where(@event =>
                    @event.EventType == EventType.Order &&
                    @event.UserId == userId)
               .ToList();
        }

        public List<Event> GetRefundsOfUser(int userId)
        {
            return _EventsRepository
               .FindAll()
               .Where(@event =>
                    @event.EventType == EventType.Refund &&
                    @event.UserId == userId)
               .ToList();
        }

        public void PlaceOrder(int userId, List<BookOrderCreate> booksToOrder)
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

            Event @event = new Event()
            {
                EventType = EventType.Order,
                PlacedDate = DateTime.UtcNow,
                UserId = userId,
                OrderedBooks = orderedBooks
            };

            var orderPlacedResult = _EventsRepository.Create(@event);

            if (!orderPlacedResult)
            {
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

            Event order = user.Orders?.FirstOrDefault(order => order.Id == eventId);
            if (order == null)
            {
                throw new PurchaseException($"Refund unavailable, user does not have order of id {eventId}");
            };

            Event refund = new Event()
            {
                EventType = EventType.Refund,
                PlacedDate = DateTime.UtcNow,
                UserId = userId,
                OrderedBooks = order.OrderedBooks
            };

            var refundPlacedResult = _EventsRepository.Create(refund);

            if (!refundPlacedResult)
            {
                throw new PurchaseException($"Refund unavailable, an unexpected error occurred.");
            }
        }
    }
}
