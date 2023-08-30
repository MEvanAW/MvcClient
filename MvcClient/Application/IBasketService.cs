using MvcClient.Dtos.Basket;
using MvcClient.Models.Basket;

namespace MvcClient.Application
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketListModel>> Filter(BasketFilterDto basketFilterDto);
        Task AddToBasket(CatalogAddToBasketDto catalogAddToBasketDto);
        Task Delete(BasketDeleteDto basketDeleteDto);
        Task Checkout(BasketCheckoutDto basketCheckooutDto);
    }
}
