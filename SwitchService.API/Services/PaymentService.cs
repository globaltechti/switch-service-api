using Polly.Retry;
using Polly;
using SwitchService.API.Entities;
using SwitchService.API.Enum;
using SwitchService.API.Models;
using System.Net;

namespace SwitchService.API.Services
{
    public class PaymentService
    {
        private readonly PaymentMethodPixService _paymentMethodPixService;
        private readonly PaymentMethodTedService _paymentMethodTedService;
        private readonly PaymentStateControlService _paymentStateControlService;
        private readonly RetryPolicy<Result<Payment>> _retryPolicy;

        public PaymentService(PaymentMethodPixService paymentMethodPixService, 
            PaymentMethodTedService paymentMethodTedService, PaymentStateControlService paymentStateControlService)
        {
                
            this._paymentMethodPixService = paymentMethodPixService;
            this._paymentMethodTedService = paymentMethodTedService;
            this._paymentStateControlService = paymentStateControlService;

            _retryPolicy = Policy
                     .HandleResult<Result<Payment>>(r => r.StatusCode != HttpStatusCode.Created)
                     .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
                     {
                         Console.WriteLine($"Payment attempt failed: {string.Join(", ", result.Result.Messages)}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                     });

        }         

        public async Task<Result<Payment>> PayAsync(Payment payment)
        {

            return _retryPolicy.Execute(() =>
            {
                var @return = new Result<Payment>();

                PaymentType paymentType = _paymentStateControlService.GetPaymentPriority();

                if (paymentType == PaymentType.NotFound)
                {

                    @return.AddMessage("There is no payment method avaliable");

                    @return.StatusCode = HttpStatusCode.ServiceUnavailable;

                    return @return;
                }

                @return.AddMessage($"Trying payment method: {paymentType}");
                var paymentResult =  PayAsync(payment, paymentType).Result;

                @return.AddMessages(paymentResult.Messages);

                @return.Content = paymentResult.Content;

                @return.StatusCode = HttpStatusCode.Created;

                return @return;
            });
        }

        private async Task<Result<Payment>> PayAsync(Payment payment, PaymentType paymentType) {

            switch (paymentType)
            {
                case PaymentType.Pix: { return await _paymentMethodPixService.PayAsync(payment); }
                case PaymentType.Ted: { return await _paymentMethodTedService.PayAsync(payment); }

                default: throw new NotImplementedException();
           }

            
        }
    }
}

