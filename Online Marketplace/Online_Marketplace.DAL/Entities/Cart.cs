using Online_Marketplace.DAL.Entities.Models;

namespace Online_Marketplace.DAL.Entities
{

    public class Cart
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }


}
