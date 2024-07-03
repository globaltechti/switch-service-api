using SwitchService.API.Services;

namespace SwitchService.API.Workers
{
    public sealed class PaymentCheckWorker(PaymentStateControlService paymentStateControlService)  : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                paymentStateControlService.CheckState();

                await Task.Delay(1_000, stoppingToken);
            }
        }
    }
}
