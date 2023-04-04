namespace Online_Marketplace.Shared.DTOs
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public string Reference { get; set; }
        public string CallbackUrl { get; set; }

    }
}
