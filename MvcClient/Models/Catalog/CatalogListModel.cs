﻿namespace MvcClient.Models.Catalog
{
    public class CatalogListModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string PictureUri { get; set; } = string.Empty;

        public string PictureFileName { get; set; } = string.Empty;

        // Quantity in stock
        public int AvailableStock { get; set; }
    }
}
