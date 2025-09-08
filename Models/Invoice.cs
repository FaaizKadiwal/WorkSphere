using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EmployeeManagementSystem.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string InvoiceNumber { get; set; } = string.Empty;

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow.Date;
        public DateTime DueDate { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        public decimal Total => InvoiceItems?.Sum(i => i.LineTotal) ?? 0m;
    }
}
