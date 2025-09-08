using System;

namespace EmployeeManagementSystem.Models
{
    public class ProjectAssignment
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow.Date;

        public string Role { get; set; } = string.Empty;
    }
}
