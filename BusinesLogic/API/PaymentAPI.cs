using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.API
{
    public class PaymentAPI : BaseAPI
    {
        private readonly List<Payment> _payments = new List<Payment>();

        public Payment CreatePayment(int orderId, PaymentMethod method, decimal amount)
        {
            var payment = new Payment(_payments.Count + 1, orderId, null, amount, PaymentStatus.Completed, method);
            _payments.Add(payment);
            return payment;
        }

        public Payment FetchPaymentByOrder(int orderId)
        {
            return _payments.FirstOrDefault(p => p.OrderId == orderId);
        }

        public Payment Refund(int paymentId)
        {
            var payment = _payments.FirstOrDefault(p => p.Id == paymentId);
            if (payment != null)
            {
                payment.Status = PaymentStatus.Refunded;
            }
            return payment;
        }
    }
}
