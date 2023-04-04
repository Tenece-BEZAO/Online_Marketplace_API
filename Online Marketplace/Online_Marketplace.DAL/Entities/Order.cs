using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Enums;
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
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string ? PaymentGateway { get; set; }
        public string ? Reference { get; set; }
        public string ? TransactionReference { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}