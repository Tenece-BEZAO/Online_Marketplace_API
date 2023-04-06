namespace Online_Marketplace.Shared.DTOs
{
    public class OrderStatusDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        public decimal Price { get; set; }
    }
}
