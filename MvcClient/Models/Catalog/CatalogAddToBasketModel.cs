using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Attribute;

namespace MvcClient.Models.Catalog
{
    public class CatalogAddToBasketModel
    {
        [Required]
        [MinLength(3)]
        public string Buyer { get; set; } = string.Empty;

        [Required]
        [HiddenInput]
        public Guid ProductId { get; set; }

        [Required]
        [HiddenInput]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        [HiddenInput]
        public decimal UnitPrice { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int AvailableStock { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Stock("AvailableStock")]
        public int Quantity { get; set; }

        public CatalogAddToBasketModel()
        {
            AvailableStock = int.MaxValue;
        }

        public CatalogAddToBasketModel(CatalogDetailsModel catalogDetailsModel)
        {
            ProductId = catalogDetailsModel.Id;
            ProductName = catalogDetailsModel.Name;
            UnitPrice = catalogDetailsModel.Price;
            AvailableStock = catalogDetailsModel.AvailableStock;
        }
    }
}
