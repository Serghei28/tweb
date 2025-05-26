using System.Collections.Generic;
using System.Threading.Tasks;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(int userId, List<OrderItem> items, int addressId);
        Order GetOrderById(int orderId);
        List<Order> GetUserOrders(int userId);
        Order UpdateOrderStatus(int orderId, OrderStatus status);
        Order CancelOrder(int orderId);
        List<Order> GetAllOrders();
        Task<bool> DeleteOrder(int orderId);

    }
}
