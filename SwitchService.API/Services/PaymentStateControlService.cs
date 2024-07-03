using SwitchService.API.Enum;

namespace SwitchService.API.Services
{
    public class PaymentStateControlService
    {
        public readonly ILogger<PaymentStateControlService> _logger;

        public PaymentStateControlService(ILogger<PaymentStateControlService> logger)
        {
            LoadPaymentPriority();
            LoadPaymentStates();
            _logger = logger;   
        }

        private IDictionary<PaymentType, PaymentState> PaymentStates;
        private IDictionary<PaymentPriority, PaymentType>  PaymentPriority;

        public void SetState(PaymentType pType, PaymentState pState) { 
        
            PaymentStates[pType] = pState;
        }

        public IEnumerable<string> GetPaymentStates() { 
            
            var paymentsStates = new List<string>();

            foreach (var state in PaymentStates) {

                paymentsStates.Add($"Payment method: {state.Key.ToString()} state: {state.Value}");
            }

            return paymentsStates;
        }

        public PaymentType GetPaymentPriority()
        {
            PaymentType paymentType = PaymentPriority[Enum.PaymentPriority.High];
            PaymentState paymentState = PaymentStates[paymentType];

            if (paymentState == PaymentState.OffLine)
            {
                 paymentType = PaymentPriority[Enum.PaymentPriority.Low];
                 paymentState = PaymentStates[paymentType];

                if (paymentState == PaymentState.OffLine) { 

                    return PaymentType.NotFound;
                }
            }

            return paymentType;
        }

        public void CheckState()
        {
          //Implement call to check if ted and or pix are avaliable
        }

        private void LoadPaymentStates()
        {

            PaymentStates = new Dictionary<PaymentType, PaymentState>();

            PaymentStates.Add(PaymentType.Pix, PaymentState.OffLine);
            PaymentStates.Add(PaymentType.Ted, PaymentState.OffLine);
        }

        private void LoadPaymentPriority()
        {

            PaymentPriority = new Dictionary<PaymentPriority, PaymentType>();

            PaymentPriority.Add(Enum.PaymentPriority.High, PaymentType.Pix);
            PaymentPriority.Add(Enum.PaymentPriority.Low, PaymentType.Ted);
        }
    }
}

