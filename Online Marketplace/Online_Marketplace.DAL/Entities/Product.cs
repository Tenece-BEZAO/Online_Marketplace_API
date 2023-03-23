using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.DAL.Entities
{
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The product description is required.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The product price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The product price must be a positive number.")]
        public decimal Price { get; set; }

        [Url(ErrorMessage = "The product image URL is not valid.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The product stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The stock quantity must be a positive number.")]
        public int StockQuantity { get; set; }

        public string Brand { get; set; }
    }
}
