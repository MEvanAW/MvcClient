using MvcClient.Models.Catalog.Brand;
using MvcClient.Models.Catalog.Type;

namespace MvcClient.Application
{
    public interface IBrandTypeService
    {
        Task<IEnumerable<BrandModel>> GetAllBrand();
        Task<IEnumerable<TypeModel>> GetAllType();
    }
}
