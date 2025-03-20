using YourProject.BusinessLogic.Interfaces;
using YourProject.BusinessLogic.Implementation;
using YourProject.BusinessLogic.Services;

namespace YourProject.BusinessLogic
{
    public class BusinessLogic
    {
        public IUserService CreateUserService()
        {
            return new UserService();
        }

        public IProductService CreateProductService()
        {
            return new ProductService();
        }

        public IPaymentService CreatePaymentService()
        {
            return new PaymentService();
        }

        public IOrderService CreateOrderService()
        {
            return new OrderService();
        }
    }
}
