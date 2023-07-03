using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string HashedPassword { get; set; } = null!;
        
        [Required]
        public string Role {get; set; } = RolesEnum.Buyer;
    }
}