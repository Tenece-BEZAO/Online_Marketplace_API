using System.ComponentModel.DataAnnotations;

namespace Online_Marketplace.Shared.DTOs
{

    public class ProductSearchDto
    {
        [Display(Name = "Search using product name")]
        public string Search { get; set; }
    }

}
