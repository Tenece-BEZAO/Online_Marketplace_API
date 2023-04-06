using Online_Marketplace.DAL.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Marketplace.DAL.Entities
{
    public class ProductReviews
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(Product))]
        public int ProductIdentity { get; set; }
        public int BuyerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }

        
        public virtual Product Product { get; set; }
        public virtual Buyer Buyer { get; set; }

    }
}
