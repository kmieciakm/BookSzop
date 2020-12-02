namespace ShopService.Models
{
    public interface IBookOrderCreate
    {
        int BookBundleId { get; set; }
        int Quantity { get; set; }
    }
}