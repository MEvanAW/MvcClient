using MvcClient.Dtos.Order;
using MvcClient.Models.Order;

namespace MvcClient.Application
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> Filter(OrderFilterDto orderFilterDto);
    }
}
