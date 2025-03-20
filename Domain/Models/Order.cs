using System;
using System.Collections.Generic;

namespace YourProject.Domain.Models
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderItem(int id, int orderId, int productId, Product product, int quantity, decimal price)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public User User { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Новый конструктор
        public Order(int id, int userId, int shippingAddressId, OrderStatus status, DateTime createdAt, List<OrderItem> items)
        {
            Id = id;
            UserId = userId;
            ShippingAddressId = shippingAddressId;
            Status = status;
            CreatedAt = createdAt;
            Items = items ?? new List<OrderItem>();
        }
    }
}
