using Online_Marketplace.DAL.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.DAL.Entities
{
    public class ProductReviews
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; }
        public virtual Buyer Buyer { get; set; }

    }
}
