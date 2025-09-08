using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        // FK -> Department
        public int DepartmentId { get; set; }

        // Navigation
        public Department? Department { get; set; }

        // Relations
        public ICollection<ProjectAssignment> ProjectAssignments { get; set; } = new List<ProjectAssignment>();
    }
}
