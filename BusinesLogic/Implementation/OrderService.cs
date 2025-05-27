using System;
using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;
using tweb.DAL.Data;
using System.Threading.Tasks;

namespace YourProject.BusinessLogic.Implementation
{
    public class OrderService : IOrderService
    {
        public Order CreateOrder(int userId, List<OrderItem> items, int addressId)
        {
            using (var db = new AppDbContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    ShippingAddressId = addressId,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Items = items,
                    TotalAmount = CalculateTotalAmount(items)
                };

                db.Orders.Add(order);
                db.SaveChanges();

                return order;
            }
        }

        public Order GetOrderById(int orderId)
        {
            using (var db = new AppDbContext())
            {
                var order = db.Orders.Include("Items").FirstOrDefault(o => o.Id == orderId);
                if (order == null) throw new Exception("Order not found");
                return order;
            }
        }

        public List<Order> GetUserOrders(int userId)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders
                         .Include("Items")
                         .Where(o => o.UserId == userId)
                         .ToList();
            }
        }

        public Order UpdateOrderStatus(int orderId, OrderStatus status)
        {
            using (var db = new AppDbContext())
            {
                var order = db.Orders.Find(orderId);
                if (order == null) throw new Exception("Order not found");

                order.Status = status;
                order.UpdatedAt = DateTime.UtcNow;
                db.SaveChanges();

                return order;
            }
        }

        public Order CancelOrder(int orderId)
        {
            using (var db = new AppDbContext())
            {
                var order = db.Orders.Find(orderId);
                if (order == null) throw new Exception("Order not found");

                if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
                    throw new Exception("Cannot cancel order that has been shipped or delivered");

                order.Status = OrderStatus.Cancelled;
                order.UpdatedAt = DateTime.UtcNow;
                db.SaveChanges();

                return order;
            }
        }
        public List<Order> GetAllOrders()
        {
            using (var db = new AppDbContext())
            {
                return db.Orders
                         .Include("User") // ← Это правильно для EF6
                         .ToList();
            }
        }


        public async Task<bool> DeleteOrder(int orderId)
        {
            using (var db = new AppDbContext())
            {
                var order = await db.Orders.FindAsync(orderId);
                if (order == null) return false;

                db.Orders.Remove(order);
                await db.SaveChangesAsync();
                return true;
            }
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
