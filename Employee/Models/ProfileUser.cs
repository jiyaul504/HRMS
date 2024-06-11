using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class ProfileUser
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string? Phone { get; set; }
    }
}
