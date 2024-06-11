using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(24, MinimumLength = 4)]
        public string Password { get; set; } = null!;
    }
}
