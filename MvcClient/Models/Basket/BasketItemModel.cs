namespace MvcClient.Models.Basket
{
    public class BasketItemModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
