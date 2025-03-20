using System;
using System.Collections.Generic;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;

namespace YourProject.BusinessLogic.Implementation
{
    public class OrderService : IOrderService
    {
        // Временное хранилище заказов для демонстрации
        private readonly List<Order> _orders = new List<Order>();

        public Order CreateOrder(int userId, List<OrderItem> items, int addressId)
        {
            int newId = _orders.Count + 1;

            var order = new Order(newId, userId, addressId, OrderStatus.Pending, DateTime.UtcNow, items);
            order.TotalAmount = CalculateTotalAmount(items);

            _orders.Add(order);
            return order;
        }


        public Order GetOrderById(int orderId)
        {
            var order = _orders.Find(o => o.Id == orderId);
            if (order == null)
                throw new Exception("Order not found");
            return order;
        }

        public List<Order> GetUserOrders(int userId)
        {
            return _orders.FindAll(o => o.UserId == userId);
        }

        public Order UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = GetOrderById(orderId);
            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;
            return order;
        }

        public Order CancelOrder(int orderId)
        {
            var order = GetOrderById(orderId);
            // Если заказ уже отправлен или доставлен, отмена невозможна
            if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
            {
                throw new Exception("Cannot cancel order that has been shipped or delivered");
            }
            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;
            return order;
        }

        private decimal CalculateTotalAmount(List<OrderItem> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }
}
