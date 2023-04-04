using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{
    public record BuyerProfileDto 
    {
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }


    }
}