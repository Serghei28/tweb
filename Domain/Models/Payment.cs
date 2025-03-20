using System;

namespace YourProject.Domain.Models
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public enum PaymentMethod
    {
        CreditCard,
        PayPal
    }

    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null;
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Payment(int id, int orderId, Order order, decimal amount, PaymentStatus status, PaymentMethod method)
        {
            Id = id;
            OrderId = orderId;
            Order = order;
            Amount = amount;
            Status = status;
            Method = method;
        }
    }
}
