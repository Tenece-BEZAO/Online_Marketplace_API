using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{

    public class CartCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

}
