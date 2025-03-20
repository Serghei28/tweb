using System;
using System.Collections.Generic;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;
using YourProject.BusinessLogic.Services;

namespace YourProject.BusinessLogic.Implementation
{
    public class PaymentService : IPaymentService
    {
        // Временное хранилище платежей для демонстрации
        private readonly List<Payment> _payments = new List<Payment>();

        public Payment ProcessPayment(int orderId, PaymentMethod method, decimal amount)
        {
            // Создаём новый платёж
            int newId = _payments.Count + 1;

            // Создаём заказ с корректным вызовом конструктора
            Order order = new Order(orderId, 0, 0, OrderStatus.Pending, DateTime.UtcNow, new List<OrderItem>());

            var payment = new Payment(newId, orderId, order, amount, PaymentStatus.Completed, method);
            _payments.Add(payment);
            return payment;
        }


        public Payment GetPaymentByOrderId(int orderId)
        {
            var payment = _payments.Find(p => p.OrderId == orderId);
            if (payment == null)
                throw new Exception("Payment not found for given order");
            return payment;
        }

        public Payment RefundPayment(int paymentId)
        {
            var payment = _payments.Find(p => p.Id == paymentId);
            if (payment == null)
                throw new Exception("Payment not found");
            if (payment.Status != PaymentStatus.Completed)
                throw new Exception("Only completed payments can be refunded");
            payment.Status = PaymentStatus.Refunded;
            payment.UpdatedAt = DateTime.UtcNow;
            return payment;
        }
    }
}
