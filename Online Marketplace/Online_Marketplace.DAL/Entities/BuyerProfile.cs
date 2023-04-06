using Online_Marketplace.DAL.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Marketplace.DAL.Entities
{
    public class BuyerProfile : BaseUserProfile
    {
        public string Address { get; set; }

        [ForeignKey(nameof(Buyer))]
        public int BuyerIdentity { get; set; }




        public virtual Buyer Buyer { get; set; }

        public List<Order> Orders { get; set; } //orders buyer placed
    }
}