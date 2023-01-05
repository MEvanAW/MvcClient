using MvcClient.Models.Catalog.Brand;
using MvcClient.Models.Catalog.Type;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MvcClient.Application
{
    public class BrandTypeService : IBrandTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        public BrandTypeService(HttpClient httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7102/");
            _logger = logger;
        }

        public async Task<IEnumerable<BrandModel>> GetAllBrands()
        {
            _logger.LogInformation("Getting all brands...");
            var httpResponseMessage = await _httpClient.GetAsync("Brand/all");
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var orderModels = data is not null
                ? ((JArray)data).ToObject<IEnumerable<BrandModel>>()
                : null;
            return orderModels is not null
                ? orderModels
                : new List<BrandModel>();
        }

        public async Task<IEnumerable<TypeModel>> GetAllTypes()
        {
            _logger.LogInformation("Getting all types...");
            var httpResponseMessage = await _httpClient.GetAsync("Type/all");
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var orderModels = data is not null
                ? ((JArray)data).ToObject<IEnumerable<TypeModel>>()
                : null;
            return orderModels is not null
                ? orderModels
                : new List<TypeModel>();
        }
    }
}
