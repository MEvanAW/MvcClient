using MvcClient.Dtos.Order;
using MvcClient.Models.Order;

namespace MvcClient.Application
{
    public interface IOrderService
    {
        IEnumerable<OrderModel> Filter(OrderFilterDto orderFilterDto);
    }
}
