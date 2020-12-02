using DatabaseManager.Models;
using ShopService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Purchase
{
    public interface IPurchaseService
    {
        void MakePurchase(int userId, List<IBookOrderCreate> booksToOrder);
        void PlaceRefund(int userId, int eventId);
        List<Event> GetAllPurchases();
        List<Event> GetAllRefunds();
        List<Event> GetUserPurchases(int userId);
        List<Event> GetUserRefunds(int userId);
    }
}
