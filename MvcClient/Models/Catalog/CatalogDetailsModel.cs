namespace MvcClient.Models.Catalog
{
    public class CatalogDetailsModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = string.Empty;

        public string PictureUri { get; set; } = string.Empty;

        public string CatalogType { get; set; } = string.Empty;

        public Guid CatalogTypeId { get; set; }

        public string CatalogBrand { get; set; } = string.Empty;

        public Guid CatalogBrandId { get; set; }

        // Quantity in stock
        public int AvailableStock { get; set; }

        // Available stock at which we should reorder
        public int RestockThreshold { get; set; }

        // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
        public int MaxStockThreshold { get; set; }

        public bool IsHidden { get; set; }

        /// <summary>
        /// True if item is on reorder
        /// </summary>
        public bool OnReorder { get; set; }
    }
}
