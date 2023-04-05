using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{
    public record SellerProfileDto 
    {
        [Required(ErrorMessage = "Business Name is required"), Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Business Description"), Required(ErrorMessage = "Description of the Business is required"), StringLength(1000, ErrorMessage = "character limit of 3 and 1000 is exceeded", MinimumLength = 3)]
        public string BusinessDescription { get; set; }

        [Display(Name = "Business Categories"), StringLength(200, ErrorMessage = "character limit of 3 and 200 is exceeded", MinimumLength = 3)]
        public string BusinessCategories { get; set; }
    }
}