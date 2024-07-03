using Microsoft.AspNetCore.Mvc;
using SwitchService.API.Entities;
using SwitchService.API.Models;
using SwitchService.API.Services;

namespace SwitchService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost(Name = "PayAsync")]
        public async Task<Result<Payment>> PayAsync([FromBody]Payment payment)
        {
            return await _paymentService.PayAsync(payment);
        }
    }
}
