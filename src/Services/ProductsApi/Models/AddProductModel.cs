using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public class AddProductModel
    {  
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(20)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Cost {get; set; }
    }
}