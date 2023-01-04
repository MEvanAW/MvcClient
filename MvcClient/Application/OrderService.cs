using System.Net.Mime;
using System.Text;
using MvcClient.Dtos.Order;
using MvcClient.Models.Order;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcClient.Application
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(HttpClient httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7019/api/order/");
            _logger = logger;
        }

        public async Task<IEnumerable<OrderModel>> Filter(OrderFilterDto orderFilterDto)
        {
            _logger.LogInformation("Filtering orders with buyer name: {buyerName}", orderFilterDto.BuyerName);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(orderFilterDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync("Orders/filter", bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var orderModels = data is not null
                ? ((JArray) data).ToObject<IEnumerable<OrderModel>>()
                : null;
            return orderModels is not null
                ? orderModels
                : new List<OrderModel>();
        }
    }
}
