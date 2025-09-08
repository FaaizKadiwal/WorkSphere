using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class ProjectAssignmentsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectAssignmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectAssignments
        public async Task<IActionResult> Index(string searchString, int? employeeId, int? projectId)
        {
            var assignments = _context.ProjectAssignments
                                      .Include(p => p.Employee)
                                      .Include(p => p.Project)
                                      .AsQueryable();

            // 🔍 Search across Employee name, Project name, Role
            if (!string.IsNullOrEmpty(searchString))
            {
                assignments = assignments.Where(a =>
                    (a.Employee != null && a.Employee.Name.Contains(searchString)) ||
                    (a.Project != null && a.Project.Name.Contains(searchString)) ||
                    (a.Role != null && a.Role.Contains(searchString)));
            }

            // ⬇️ Filter by Employee
            if (employeeId.HasValue)
            {
                assignments = assignments.Where(a => a.EmployeeId == employeeId.Value);
            }

            // ⬇️ Filter by Project
            if (projectId.HasValue)
            {
                assignments = assignments.Where(a => a.ProjectId == projectId.Value);
            }

            // Populate dropdowns
            ViewBag.Employees = new SelectList(_context.Employees, "Id", "Name");
            ViewBag.Projects = new SelectList(_context.Projects, "Id", "Name");

            return View(await assignments.ToListAsync());
        }


        // GET: ProjectAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var projectAssignment = await _context.ProjectAssignments
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (projectAssignment == null) return NotFound();

            return View(projectAssignment);
        }

        // GET: ProjectAssignments/Create
        public IActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_context.Employees, "Id", "Name");
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: ProjectAssignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,ProjectId,AssignedDate,Role")] ProjectAssignment projectAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EmployeeId = new SelectList(_context.Employees, "Id", "Name", projectAssignment.EmployeeId);
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Name", projectAssignment.ProjectId);
            return View(projectAssignment);
        }

        // GET: ProjectAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var projectAssignment = await _context.ProjectAssignments.FindAsync(id);
            if (projectAssignment == null) return NotFound();

            ViewBag.EmployeeId = new SelectList(_context.Employees, "Id", "Name", projectAssignment.EmployeeId);
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Name", projectAssignment.ProjectId);
            return View(projectAssignment);
        }

        // POST: ProjectAssignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,ProjectId,AssignedDate,Role")] ProjectAssignment projectAssignment)
        {
            if (id != projectAssignment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectAssignmentExists(projectAssignment.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EmployeeId = new SelectList(_context.Employees, "Id", "Name", projectAssignment.EmployeeId);
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Name", projectAssignment.ProjectId);
            return View(projectAssignment);
        }

        // GET: ProjectAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var projectAssignment = await _context.ProjectAssignments
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (projectAssignment == null) return NotFound();

            return View(projectAssignment);
        }

        // POST: ProjectAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectAssignment = await _context.ProjectAssignments.FindAsync(id);
            if (projectAssignment != null)
            {
                _context.ProjectAssignments.Remove(projectAssignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProjectAssignmentExists(int id)
        {
            return _context.ProjectAssignments.Any(e => e.Id == id);
        }
    }
}
