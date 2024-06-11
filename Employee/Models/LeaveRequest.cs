using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }
        public string Status { get; set; }

        public string Type { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
       
    }
    public enum ApprovalStatus
    {
        Pending,
        ApprovedLevel1,
        ApprovedLevel2,
        ApprovedLevel3,
        Rejected
    }
}
