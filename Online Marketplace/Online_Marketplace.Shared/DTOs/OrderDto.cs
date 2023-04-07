namespace Online_Marketplace.Shared.DTOs
{
    public class OrderDto
    {

        public decimal Total { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
