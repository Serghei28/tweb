using System.Collections.Generic;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.API
{
    public class OrderAPI : BaseAPI
    {
        public OrderAPI() : base() { }

        public Order GetOrder(int id)
        {
            return new Order(id, 1, 1, OrderStatus.Pending, System.DateTime.UtcNow, new List<OrderItem>());
        }
    }
}
