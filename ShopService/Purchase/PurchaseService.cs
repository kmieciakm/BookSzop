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

        public PurchaseService(IEventsRepository eventsRepository)
        {
            _EventsRepository = eventsRepository;
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
            throw new NotImplementedException();
        }

        public void PlaceRefund(int userId, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
