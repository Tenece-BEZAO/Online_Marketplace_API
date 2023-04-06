using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.DAL.Entities
{
    public class Shipping
    {

        [Key]
        public int Id { get; set; }

        public string ? Name { get; set; }

       /* public int SellerId { get; set; }

        public Seller Seller { get; set; }*/

        public decimal Rate { get; set; }

        public DateTime EstimateDeliveryDate { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public string ? Policy { get; set; }


    }
}

