using System.Diagnostics;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    // ✅ Require authentication for all actions
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // ✅ Quick Stats
            ViewBag.EmployeeCount = _context.Employees.Count();
            ViewBag.DepartmentCount = _context.Departments.Count();
            ViewBag.ClientCount = _context.Clients.Count();
            ViewBag.ProjectCount = _context.Projects.Count();
            ViewBag.ProjectAssignmentCount = _context.ProjectAssignments.Count();
            ViewBag.InvoiceCount = _context.Invoices.Count();
            ViewBag.InvoiceItemCount = _context.InvoiceItems.Count();

            // ✅ Employees per Department (Pie Chart)
            var deptNames = _context.Departments
                .Select(d => d.Name)
                .ToList();

            var employeesPerDept = _context.Departments
                .Select(d => d.Employees.Count())
                .ToList();

            ViewBag.DepartmentNames = deptNames;
            ViewBag.EmployeePerDepartment = employeesPerDept;

            // ✅ Clients Growth (Line Chart) – Mock Data (replace with CreatedDate if exists)
            var now = DateTime.Now;
            var last6Months = Enumerable.Range(0, 6)
                .Select(i => now.AddMonths(-i))
                .OrderBy(d => d)
                .ToList();

            var clientGrowthLabels = last6Months
                .Select(d => d.ToString("MMM yyyy"))
                .ToList();

            var random = new Random();
            var clientGrowthValues = last6Months
                .Select(_ => random.Next(1, 20))
                .ToList();

            ViewBag.ClientMonths = clientGrowthLabels;
            ViewBag.ClientGrowth = clientGrowthValues;

            // ✅ Invoices per Client (Bar Chart)
            var invoiceClients = _context.Clients
                .Select(c => c.Name)
                .ToList();

            var invoicesPerClient = _context.Clients
                .Select(c => c.Invoices.Count())
                .ToList();

            ViewBag.InvoiceClients = invoiceClients;
            ViewBag.InvoicesPerClient = invoicesPerClient;

            // ✅ Projects per Department (Doughnut Chart)
            var projectDeptNames = _context.Departments
                .Select(d => d.Name)
                .ToList();

            var projectsPerDept = _context.Departments
                .Select(d => d.Projects.Count())
                .ToList();

            ViewBag.ProjectDeptNames = projectDeptNames;
            ViewBag.ProjectsPerDepartment = projectsPerDept;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
