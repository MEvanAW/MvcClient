namespace MvcClient.Models.Basket
{
    public class BasketListModel
    {
        public Guid Id { get; set; }
        public string Buyer { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
