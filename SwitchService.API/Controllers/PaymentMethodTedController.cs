using Microsoft.AspNetCore.Mvc;
using SwitchService.API.Enum;
using SwitchService.API.Services;

namespace SwitchService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentMethodTedController : ControllerBase
    {
        
        private readonly PaymentStateControlService _paymentStateControlService;

        public PaymentMethodTedController(PaymentStateControlService paymentStateControlService)
        {
            _paymentStateControlService = paymentStateControlService;
        }
        

        [HttpPut(Name = "status-ted")]
        public PaymentState TedChangeState([FromBody]PaymentState paymentState)
        {
            _paymentStateControlService.SetState(PaymentType.Ted, paymentState);

            return paymentState;
        }
    }
}
