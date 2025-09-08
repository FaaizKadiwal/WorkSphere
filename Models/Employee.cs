using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        // Foreign key for Department
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }

        // Navigation property
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        // Navigation for Project Assignments
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; } = new List<ProjectAssignment>();
    }
}
