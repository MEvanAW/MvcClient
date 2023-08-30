using MvcClient.Enums;

namespace MvcClient.Models.Order
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
