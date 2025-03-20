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
        public List<string> Images { get; set; } = new List<string>();
        public Dictionary<string, string> Specs { get; set; } = new Dictionary<string, string>();
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Product(int id, string name, string description, decimal price, ProductCategory category, List<string> images, Dictionary<string, string> specs, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Images = images ?? new List<string>();
            Specs = specs ?? new Dictionary<string, string>();
            Stock = stock;
        }
    }
}
