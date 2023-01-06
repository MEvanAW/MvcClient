using System.Net.Mime;
using System.Text;
using MvcClient.Dtos.Basket;
using MvcClient.Models.Basket;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MvcClient.Application
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BasketService> _logger;
        private const string _filter = "/api/Basket";
        private const string _add = "/api/Basket/add";

        public BasketService(HttpClient httpClient, ILogger<BasketService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7106");
            _logger = logger;
        }

        public async Task<IEnumerable<BasketListModel>> Filter(BasketFilterDto basketFilterDto)
        {
            _logger.LogInformation("Filtering basket with buyer \"{name}\"", basketFilterDto.Buyer);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(basketFilterDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(_filter, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var basketModels = data is not null
                ? ((JArray)data).ToObject<IEnumerable<BasketListModel>>()
                : null;
            return basketModels is not null
                ? basketModels
                : new List<BasketListModel>();
        }

        public async Task AddToBasket(CatalogAddToBasketDto catalogAddToBasketDto)
        {
            _logger.LogInformation("Adding catalog to basket with buyer \"{name}\" and product \"{product}\"", catalogAddToBasketDto.Buyer, catalogAddToBasketDto.ProductName);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogAddToBasketDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(_add, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
