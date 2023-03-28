using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.Shared.DTOs
{
   
    public class ProductSearchDto
    {
        [Display (Name = "Search using product name")]
        public string Search { get; set; }
    }

}
