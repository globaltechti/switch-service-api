namespace SwitchService.API.Entities
{
    public class Payment
    {
        public Payment()
        {
            Id = Guid.NewGuid();
        }

        public decimal Value { get; set; }
        public Guid Id { get; private set; }

    }
}
