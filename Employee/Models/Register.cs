using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Register
    {
       
        public string FirstName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(24, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}
