using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Online_Marketplace.DAL.Entities.Models;
using System.Collections.Generic;
using Online_Marketplace.DAL.Entities;

namespace Online_Marketplace.DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public string Description { get; set; }


        public decimal Price { get; set; }


        public int StockQuantity { get; set; }


        public string Brand { get; set; }


        [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }


        public Seller Seller { get; set; }

        public virtual ICollection<ProductReviews>  ProductReview {get;set;}
    }
}