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
    public class CorrespondencesController : Controller
    {
        private readonly QAccessContext _context;

        public CorrespondencesController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Correspondences
        public async Task<IActionResult> Index()
        {
            var qAccessContext = _context.Correspondences.Include(c => c.EmployeeDelivery).Include(c => c.Unit);
            return View(await qAccessContext.ToListAsync());
        }

        // GET: Correspondences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.Correspondences
                .Include(c => c.EmployeeDelivery)
                .Include(c => c.Unit)
                .FirstOrDefaultAsync(m => m.CorrespondenceId == id);
            if (correspondence == null)
            {
                return NotFound();
            }

            return View(correspondence);
        }

        // GET: Correspondences/Create
        public IActionResult Create()
        {
            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber");
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block");
            return View();
        }

        // POST: Correspondences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorrespondenceId,TrackingCode,Status,Sender,UnitId,DateDelivery,EmployeeDeliveryId")] Correspondence correspondence)
        {
            Employee employee = await _context.Employees.FindAsync(correspondence.EmployeeDeliveryId);
            correspondence.EmployeeDelivery = employee;
            Unit unit = await _context.Units.FindAsync(correspondence.UnitId);
            correspondence.Unit = unit;
            if (ModelState.IsValid)
            {
                _context.Add(correspondence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }else
            {
                var messsage = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var item in messsage)
                {
                    ModelState.AddModelError("", item);
                }
            }
            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "Name", correspondence.EmployeeDeliveryId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Number", correspondence.UnitId);
            return View(correspondence);
        }

        // GET: Correspondences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.Correspondences.FindAsync(id);
            if (correspondence == null)
            {
                return NotFound();
            }
            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", correspondence.EmployeeDeliveryId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block", correspondence.UnitId);
            return View(correspondence);
        }

        // POST: Correspondences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CorrespondenceId,TrackingCode,Status,Sender,UnitId,DateDelivery,EmployeeDeliveryId,EmployeeWithdrawalId")] Correspondence correspondence)
        {
            if (id != correspondence.CorrespondenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correspondence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrespondenceExists(correspondence.CorrespondenceId))
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
            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", correspondence.EmployeeDeliveryId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block", correspondence.UnitId);
            return View(correspondence);
        }

        // GET: Correspondences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.Correspondences
                .Include(c => c.EmployeeDelivery)
                .Include(c => c.Unit)
                .FirstOrDefaultAsync(m => m.CorrespondenceId == id);
            if (correspondence == null)
            {
                return NotFound();
            }

            return View(correspondence);
        }

        // POST: Correspondences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Correspondences == null)
            {
                return Problem("Entity set 'QAccessContext.Correspondences'  is null.");
            }
            var correspondence = await _context.Correspondences.FindAsync(id);
            if (correspondence != null)
            {
                _context.Correspondences.Remove(correspondence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrespondenceExists(int id)
        {
          return (_context.Correspondences?.Any(e => e.CorrespondenceId == id)).GetValueOrDefault();
        }
    }
}
