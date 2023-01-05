using System.Net.Mime;
using System.Text;
using MvcClient.Dtos.Catalog;
using MvcClient.Models.Catalog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcClient.Application
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        public CatalogService(HttpClient httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7102/Catalog");
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogListModel>> Filter(CatalogFilterDto catalogFilterDto)
        {
            _logger.LogInformation("Filtering catalog with Name \"{name}\" and Description \"{description}\"", catalogFilterDto.Name, catalogFilterDto.Description);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogFilterDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(string.Empty, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var orderModels = data is not null
                ? ((JArray)data).ToObject<IEnumerable<CatalogListModel>>()
                : null;
            return orderModels is not null
                ? orderModels
                : new List<CatalogListModel>();
        }
    }
}
