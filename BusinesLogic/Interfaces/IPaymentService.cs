using YourProject.Domain.Models; // Убедимся, что пространство имен подключено

namespace YourProject.BusinessLogic.Services
{
    public interface IPaymentService
    {
        Payment ProcessPayment(int orderId, PaymentMethod method, decimal amount);
        Payment GetPaymentByOrderId(int orderId);
        Payment RefundPayment(int paymentId);
    }
}
