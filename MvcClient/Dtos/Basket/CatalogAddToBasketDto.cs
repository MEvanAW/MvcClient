using MvcClient.Models.Catalog;

namespace MvcClient.Dtos.Basket
{
    public class CatalogAddToBasketDto
    {
        public string Buyer { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public CatalogAddToBasketDto() { }
        public CatalogAddToBasketDto(CatalogAddToBasketModel catalogAddToBasketModel)
        {
            Buyer = catalogAddToBasketModel.Buyer;
            ProductId = catalogAddToBasketModel.ProductId;
            ProductName = catalogAddToBasketModel.ProductName;
            UnitPrice = catalogAddToBasketModel.UnitPrice;
            Quantity = catalogAddToBasketModel.Quantity;
        }
    }
}
