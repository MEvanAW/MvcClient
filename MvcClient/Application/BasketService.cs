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
        private const string _delete = "/api/Basket/delete";
        private const string _checkout = "/api/Basket/checkout";

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
            var basketItems = data is not null
                ? ((JArray)data).ToObject<List<BasketItemListModel>>()
                : null;
            var baskets = new List<BasketListModel>();
            if (basketItems is null || basketItems.Count <= 0)
            {
                return baskets;
            }
            basketItems.Sort(delegate(BasketItemListModel a, BasketItemListModel b)
            {
                return a.Buyer.CompareTo(b.Buyer);
            });
            string buyer = string.Empty;
            int basketIndex = -1;
            foreach (var item in basketItems)
            {
                if (item.Buyer != buyer)
                {
                    baskets.Add(new BasketListModel
                    {
                        Buyer = item.Buyer,
                        Items = new List<BasketItemModel>()
                        {
                            item
                        }
                    });
                    buyer = item.Buyer;
                    basketIndex++;
                }
                else
                {
                    ((List<BasketItemModel>) baskets[basketIndex].Items!).Add(item);
                }
            }
            return baskets;
        }

        public async Task AddToBasket(CatalogAddToBasketDto catalogAddToBasketDto)
        {
            _logger.LogInformation("Adding catalog to basket with buyer \"{name}\" and product \"{product}\"", catalogAddToBasketDto.Buyer, catalogAddToBasketDto.ProductName);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogAddToBasketDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(_add, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task Delete(BasketDeleteDto basketDeleteDto)
        {
            _logger.LogInformation("Deleting basket with buyer \"{name}\" and id \"{id}\"", basketDeleteDto.Buyer, basketDeleteDto.CacheId);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(basketDeleteDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(_delete, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task Checkout(BasketCheckoutDto basketCheckooutDto)
        {
            _logger.LogInformation("Checking out {name}'s basket", basketCheckooutDto.Buyer);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(basketCheckooutDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(_checkout, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
