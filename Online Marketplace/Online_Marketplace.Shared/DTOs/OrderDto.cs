using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.Shared.DTOs
{
    public class OrderDto
    {
      
        public decimal Total { get; set; }
        /*public string Status { get; set; }*/
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
