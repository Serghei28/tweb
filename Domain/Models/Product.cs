using System;
using System.Collections.Generic;

namespace YourProject.Domain.Models
{
    public enum ProductCategory
    {
        Mac,
        iPhone,
        iPad
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

        // Только одно свойство для хранения URL картинки
        public string ImageUrl { get; set; } = "/Content/Images/default.png";

        // Технические характеристики (по желанию)
        public Dictionary<string, string> Specs { get; set; } = new Dictionary<string, string>();

        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Product() { }

        public Product(int id, string name, string description, decimal price, ProductCategory category, string imageUrl, Dictionary<string, string> specs, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? "/Content/Images/default.png" : imageUrl;
            Specs = specs ?? new Dictionary<string, string>();
            Stock = stock;
        }
    }
}
