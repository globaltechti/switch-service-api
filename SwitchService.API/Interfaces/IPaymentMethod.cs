using SwitchService.API.Entities;
using SwitchService.API.Models;

namespace SwitchService.API.Interfaces
{
    public interface IPaymentMethod
    {
        Task<Result<Payment>> PayAsync(Payment payment);
    }
}
