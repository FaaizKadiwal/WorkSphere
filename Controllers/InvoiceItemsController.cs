using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class InvoiceItemsController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceItems
        public async Task<IActionResult> Index(string searchString, int? invoiceId)
        {
            var items = _context.InvoiceItems
                                .Include(i => i.Invoice)
                                .AsQueryable();

            // 🔍 Search by description
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Description.Contains(searchString));
            }

            // 📂 Filter by invoice
            if (invoiceId.HasValue)
            {
                items = items.Where(i => i.InvoiceId == invoiceId);
            }

            // Populate invoice dropdown
            ViewBag.Invoices = new SelectList(_context.Invoices, "Id", "InvoiceNumber");

            return View(await items.ToListAsync());
        }

        // GET: InvoiceItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var invoiceItem = await _context.InvoiceItems
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoiceItem == null) return NotFound();

            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber");
            return View();
        }

        // POST: InvoiceItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceId,Description,Quantity,UnitPrice")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var invoiceItem = await _context.InvoiceItems.FindAsync(id);
            if (invoiceItem == null) return NotFound();

            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceId,Description,Quantity,UnitPrice")] InvoiceItem invoiceItem)
        {
            if (id != invoiceItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceItemExists(invoiceItem.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var invoiceItem = await _context.InvoiceItems
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoiceItem == null) return NotFound();

            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceItem = await _context.InvoiceItems.FindAsync(id);
            if (invoiceItem != null)
            {
                _context.InvoiceItems.Remove(invoiceItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceItemExists(int id)
        {
            return _context.InvoiceItems.Any(e => e.Id == id);
        }
    }
}
