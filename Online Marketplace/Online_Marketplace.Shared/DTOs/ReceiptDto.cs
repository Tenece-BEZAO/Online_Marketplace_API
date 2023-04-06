namespace Online_Marketplace.Shared.DTOs
{
    public class ReceiptDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ReceiptItemDto> Items { get; set; }
    }
}
