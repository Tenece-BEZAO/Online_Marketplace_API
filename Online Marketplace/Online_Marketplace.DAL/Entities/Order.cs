using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.DAL.Entities
{
    public class Order
    {
        public int UserId { get; set; }

        public int TotalAmount { get; set; }

        [ForeignKey(nameof(OrderItems))]
        public int OrderItemsId { get; set; }
        public OrderItems OrderItems { get; set; }
    }

    
}
