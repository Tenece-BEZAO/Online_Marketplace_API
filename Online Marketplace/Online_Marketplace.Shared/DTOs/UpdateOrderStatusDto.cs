namespace Online_Marketplace.Shared.DTOs
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }
    }
}
