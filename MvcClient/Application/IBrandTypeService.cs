using MvcClient.Models.Catalog.Brand;
using MvcClient.Models.Catalog.Type;

namespace MvcClient.Application
{
    public interface IBrandTypeService
    {
        Task<IEnumerable<BrandModel>> GetAllBrands();
        Task<IEnumerable<TypeModel>> GetAllTypes();
    }
}
