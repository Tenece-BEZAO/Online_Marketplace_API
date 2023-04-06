using Online_Marketplace.DAL.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Marketplace.DAL.Entities
{
    public class SellerProfile : BaseUserProfile
    {

        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public string BusinessCatagories { get; set; }

        [ForeignKey(nameof(Seller))]
        public int SellerIdentity { get; set; }



        public Seller Seller { get; set; }
        public List<Product> Products { get; set; } //List of seller's product
        public List<Order> Orders { get; set; } //List of orders received 
    }
}