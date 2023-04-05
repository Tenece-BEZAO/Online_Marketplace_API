namespace Online_Marketplace.Shared.DTOs
{
    public class OrderItemDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
