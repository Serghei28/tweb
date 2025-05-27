using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Activation;

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

        [Required(ErrorMessage = "Укажите название товара")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите описание")]
        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public ProductCategory Category { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть 0 или больше")]
        public int Stock { get; set; }

        [System.ComponentModel.DataAnnotations.Url(ErrorMessage = "Некорректный формат URL")]
        public string ImageUrl { get; set; } = "/Content/Images/default.png";

        // Характеристики товара (опционально)
        public Dictionary<string, string> Specs { get; set; } = new Dictionary<string, string>();

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
