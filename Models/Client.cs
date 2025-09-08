using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        // Relations
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        // ✅ Departments handling this client
        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
