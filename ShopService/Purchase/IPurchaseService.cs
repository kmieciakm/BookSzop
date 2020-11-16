using DatabaseManager.Models;
using ShopService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Purchase
{
    public interface IPurchaseService
    {
        void PlaceOrder(int userId, List<BookOrderCreate> booksToOrder);
        void PlaceRefund(int userId, int eventId);
        List<Event> GetAllOrders();
        List<Event> GetAllRefunds();
        List<Event> GetOrdersOfUser(int userId);
        List<Event> GetRefundsOfUser(int userId);
    }
}
