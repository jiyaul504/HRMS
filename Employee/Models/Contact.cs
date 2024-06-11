using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;
    }
}
