using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{
    public record SellerProfileDto : UserForRegistrationDto
    {
        [Required(ErrorMessage = "Business Name is required")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "Description of the Business is required"), StringLength(1000, ErrorMessage = "character limit of 3 and 1000 is exceeded", MinimumLength = 3)]
        public string BusinessDescription { get; set; }
    }
}