using MvcClient.Dtos.Catalog;
using MvcClient.Models.Catalog;

namespace MvcClient.Application
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogListModel>> Filter(CatalogFilterDto catalogFilterDto);
        Task<CatalogDetailsModel> Details(CatalogDetailsDto catalogDetailsDto);
    }
}
