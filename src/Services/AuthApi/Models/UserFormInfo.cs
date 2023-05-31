using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models
{
    public class UserFormInfo
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;
    }
}