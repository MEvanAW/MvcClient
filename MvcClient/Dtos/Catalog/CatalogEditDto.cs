using MvcClient.Models.Catalog;

namespace MvcClient.Dtos.Catalog
{
    public class CatalogEditDto : CatalogCreateDto
    {
        public Guid Id { get; set; }

        public CatalogEditDto() { }

        public CatalogEditDto(CatalogDetailsModel catalogDetailsModel)
        {
            Name = catalogDetailsModel.Name;
            Description = catalogDetailsModel.Description;
            Price = catalogDetailsModel.Price;
            PictureFileName = catalogDetailsModel.PictureFileName;
            PictureUri = catalogDetailsModel.PictureUri;
            CatalogTypeId = catalogDetailsModel.CatalogTypeId;
            CatalogBrandId = catalogDetailsModel.CatalogBrandId;
            AvailableStock = catalogDetailsModel.AvailableStock;
            RestockThreshold = catalogDetailsModel.RestockThreshold;
            MaxStockThreshold = catalogDetailsModel.MaxStockThreshold;
            OnReorder = catalogDetailsModel.OnReorder;
            IsHidden = catalogDetailsModel.IsHidden;
            Id = catalogDetailsModel.Id;
        }
    }
}
