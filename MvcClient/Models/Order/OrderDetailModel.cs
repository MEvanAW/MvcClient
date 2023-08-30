using MvcClient.Dtos.Order;
using MvcClient.Enums;

namespace MvcClient.Models.Order
{
    public class OrderDetailModel
    {
        public Guid Id { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = Enumerable.Empty<OrderItemDto>();
    }
}
