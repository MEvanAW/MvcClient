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
        private readonly ILogger<CatalogService> _logger;
        private const string _catalog = "/Catalog";

        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7102");
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogListModel>> Filter(CatalogFilterDto catalogFilterDto)
        {
            _logger.LogInformation("Filtering catalog with Name \"{name}\" and Description \"{description}\"", catalogFilterDto.Name, catalogFilterDto.Description);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogFilterDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync(_catalog, bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var catalogModels = data is not null
                ? ((JArray)data).ToObject<IEnumerable<CatalogListModel>>()
                : null;
            return catalogModels is not null
                ? catalogModels
                : new List<CatalogListModel>();
        }

        public async Task<CatalogDetailsModel> Details(CatalogDetailsDto catalogDetailsDto)
        {
            _logger.LogInformation("Getting catalog details with guid: {guid}", catalogDetailsDto.Id);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogDetailsDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync("/Catalog/details", bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var catalogDetails = data is not null
                ? ((JObject)data).ToObject<CatalogDetailsModel>()
                : null;
            return catalogDetails is not null
                ? catalogDetails
                : new CatalogDetailsModel
                {
                    Id = catalogDetailsDto.Id
                };
        }

        public async Task Create(CatalogCreateDto catalogCreateDto)
        {
            _logger.LogInformation("Create a new catalog with name: {name}", catalogCreateDto.Name);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogCreateDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync("/Catalog/create", bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task Update(CatalogEditDto catalogEditDto)
        {
            _logger.LogInformation("Update catalog with guid: {guid}", catalogEditDto.Id);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogEditDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync("/Catalog/update", bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<string> Delete(CatalogDeleteDto catalogDeleteDto)
        {
            _logger.LogInformation("Deleting catalog with guid: {guid}", catalogDeleteDto.Id);
            var bodyJson = new StringContent(JsonConvert.SerializeObject(catalogDeleteDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponseMessage = await _httpClient.PostAsync("/Catalog/delete", bodyJson);
            httpResponseMessage.EnsureSuccessStatusCode();
            string? responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var dictionary = responseString is not null
                ? JsonConvert.DeserializeObject<IDictionary<string, object?>>(responseString)
                : null;
            var data = dictionary is not null
                ? dictionary["data"]
                : null;
            var catalog = data is not null
                ? ((JObject)data).ToObject<CatalogFilterDto>()
                : null;
            return catalog is not null
                ? catalog.Name
                : string.Empty;
        }
    }
}
