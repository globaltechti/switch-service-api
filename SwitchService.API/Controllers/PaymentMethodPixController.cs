using Microsoft.AspNetCore.Mvc;
using SwitchService.API.Enum;
using SwitchService.API.Services;

namespace SwitchService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentMethodPixController : ControllerBase
    {
        
        private readonly PaymentStateControlService _paymentStateControlService;

        public PaymentMethodPixController(PaymentStateControlService paymentStateControlService)
        {
            _paymentStateControlService = paymentStateControlService;
        }    

        [HttpPut(Name = "status-pix")]
        public PaymentState PixChangeState([FromBody]PaymentState paymentState)
        {
            _paymentStateControlService.SetState(PaymentType.Pix, paymentState);

            return paymentState;
        }
      
    }
}
