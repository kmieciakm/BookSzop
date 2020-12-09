using ShopService.Models.BookOrderModel;
using ShopService.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Purchase
{
    public interface IPurchaseService
    {
        void MakePurchase(int userId, IEnumerable<IBookOrderCreate> booksToOrder);
        void PlaceRefund(int userId, int eventId);
        IEnumerable<IEvent> GetAllPurchases();
        IEnumerable<IEvent> GetAllRefunds();
        IEnumerable<IEvent> GetUserPurchases(int userId);
        IEnumerable<IEvent> GetUserRefunds(int userId);
    }
}
