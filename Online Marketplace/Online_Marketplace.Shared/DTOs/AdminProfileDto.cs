using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{
    public record AdminProfileDto : UserForRegistrationDto
    {

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; init; }
    }
}