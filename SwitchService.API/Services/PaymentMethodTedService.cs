using SwitchService.API.Entities;
using SwitchService.API.Enum;
using SwitchService.API.Models;

namespace SwitchService.API.Services
{
    public class PaymentMethodTedService
    {
        public async Task<Result<Payment>> PayAsync(Payment payment)
        {
            var paymentResult = new Result<Payment>()
            {
                Content = payment,            
            };

            paymentResult.AddMessage($"Paid by {PaymentType.Ted.ToString()}");

            return paymentResult;
        }
    }
}
