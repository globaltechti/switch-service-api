using Microsoft.AspNetCore.Mvc;
using SwitchService.API.Services;

namespace SwitchService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentMethodController : ControllerBase
    {
        
        private readonly PaymentStateControlService _paymentStateControlService;

        public PaymentMethodController(PaymentStateControlService paymentStateControlService)
        {
            _paymentStateControlService = paymentStateControlService;
        }

        [HttpGet(Name = "")]
        public IEnumerable<string> GetPaymentMethodState()
        {
            return _paymentStateControlService.GetPaymentStates();
        }
    }
}
