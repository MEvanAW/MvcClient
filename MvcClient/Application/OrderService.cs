using MvcClient.Dtos.Order;
using MvcClient.Models.Order;

namespace MvcClient.Application
{
    public class OrderService : IOrderService
    {
        public IEnumerable<OrderModel> Filter(OrderFilterDto orderFilterDto)
        {
            return new List<OrderModel>();
        }
    }
}
