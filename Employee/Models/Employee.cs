using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Employee Code")]
        public string EmpCode { get; set; }

        [Required]
        [Range(18, 65, ErrorMessage = "Please enter a valid age.")]
        [Display(Name = "Date Of Birth")]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name="Manager")]
        public string Manager { get; set; }
    }

}
