using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "stock is required")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "brand is required")]
        public string Brand { get; set; }


    }
}


