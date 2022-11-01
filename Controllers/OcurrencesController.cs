using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QAccess.Models;

namespace QAccess.Controllers
{
    public class OcurrencesController : Controller
    {
        private readonly QAccessContext _context;

        public OcurrencesController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Ocurrences
        public async Task<IActionResult> Index()
        {
            var qAccessContext = _context.Ocurrences.Include(o => o.Responsable).Include(o => o.ResponsibleOfficial);
            return View(await qAccessContext.ToListAsync());
        }

        // GET: Ocurrences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ocurrences == null)
            {
                return NotFound();
            }

            var ocurrence = await _context.Ocurrences
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial)
                .FirstOrDefaultAsync(m => m.OcurrenceId == id);
            if (ocurrence == null)
            {
                return NotFound();
            }

            return View(ocurrence);
        }

        // GET: Ocurrences/Create
        public IActionResult Create()
        {
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber");
            return View();
        }

        // POST: Ocurrences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OcurrenceId,Locale,Status,CondominiumId,EmployeeId,Date,Description,Answer,Title,PhotoBase64")] Ocurrence ocurrence)
        {
            Condominium condominium = _context.Codominiums.Find(ocurrence.CondominiumId);
            ocurrence.Responsable = condominium;
            Employee employee = _context.Employees.Find(ocurrence.EmployeeId);
            ocurrence.ResponsibleOfficial = employee;
            if (ModelState.IsValid)
            {
                _context.Add(ocurrence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber", ocurrence.CondominiumId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", ocurrence.EmployeeId);
            return View(ocurrence);
        }

        // GET: Ocurrences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ocurrences == null)
            {
                return NotFound();
            }

            var ocurrence = await _context.Ocurrences.FindAsync(id);
            if (ocurrence == null)
            {
                return NotFound();
            }
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber", ocurrence.CondominiumId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", ocurrence.EmployeeId);
            return View(ocurrence);
        }

        // POST: Ocurrences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OcurrenceId,Locale,Status,CondominiumId,EmployeeId,Date,Description,Answer,Title,PhotoBase64")] Ocurrence ocurrence)
        {
            if (id != ocurrence.OcurrenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocurrence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcurrenceExists(ocurrence.OcurrenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber", ocurrence.CondominiumId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", ocurrence.EmployeeId);
            return View(ocurrence);
        }

        // GET: Ocurrences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ocurrences == null)
            {
                return NotFound();
            }

            var ocurrence = await _context.Ocurrences
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial)
                .FirstOrDefaultAsync(m => m.OcurrenceId == id);
            if (ocurrence == null)
            {
                return NotFound();
            }

            return View(ocurrence);
        }

        // POST: Ocurrences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ocurrences == null)
            {
                return Problem("Entity set 'QAccessContext.Ocurrences'  is null.");
            }
            var ocurrence = await _context.Ocurrences.FindAsync(id);
            if (ocurrence != null)
            {
                _context.Ocurrences.Remove(ocurrence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcurrenceExists(int id)
        {
          return (_context.Ocurrences?.Any(e => e.OcurrenceId == id)).GetValueOrDefault();
        }
    }
}
