using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public class UpdateProductModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; } 

        [MinLength(20)]
        public string? Description { get; set; }

        public decimal Cost { get; set; }
    }
}