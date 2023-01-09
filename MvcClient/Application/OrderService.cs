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
                ? ((JArray) data).ToObject<List<OrderModel>>()
                : null;
            if (orderModels is null)
            {
                return new List<OrderModel>();
            }
            orderModels.Sort(delegate (OrderModel a, OrderModel b)
            {
                return - a.CreatedDateTime.CompareTo(b.CreatedDateTime);
            });
            return orderModels;
        }

        public async Task<OrderDetailModel> Details(OrderDetailDto orderDetailDto)
        {
            _logger.LogInformation("Getting order details with guid: {guid}", orderDetailDto.Id);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(orderDetailDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync("Orders/details", bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var orderDetails = data is not null
                ? ((JObject)data).ToObject<OrderDetailModel>()
                : null;
            return orderDetails is not null
                ? orderDetails
                : new OrderDetailModel {
                    Id= orderDetailDto.Id
                };
        }

        public async Task Delete()
        {
            _logger.LogInformation("Deleting expired orders...");
            var httpResponseMessage = await _httpClient.GetAsync("Crons/initiate");
            httpResponseMessage.EnsureSuccessStatusCode();
            return;
        }
    }
}
