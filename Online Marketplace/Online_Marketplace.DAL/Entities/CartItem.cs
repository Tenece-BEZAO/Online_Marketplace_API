using Online_Marketplace.DAL.Enums;

namespace Online_Marketplace.DAL.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        

    }

}
