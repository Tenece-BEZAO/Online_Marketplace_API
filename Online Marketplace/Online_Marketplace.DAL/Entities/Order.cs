using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Enums;

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

        public string ? shippingmethod { get; set; }  

        public decimal ShippingCost { get; set; }

        public DateTime EstimateDeliveryDate { get; set; }


    }

}