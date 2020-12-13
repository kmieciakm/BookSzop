namespace ShopService.Models.BookOrderModel
{
    public interface IBookOrderCreate
    {
        int BookBundleId { get; set; }
        int Quantity { get; set; }
    }
}