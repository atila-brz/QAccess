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
    public class OcurrenceController : Controller
    {
        private readonly QAccessContext _context;

        public OcurrenceController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Ocurrence
        public async Task<IActionResult> Index()
        {
              return _context.ocurrences != null ? 
                          View(await _context.ocurrences.ToListAsync()) :
                          Problem("Entity set 'QAccessContext.ocurrences'  is null.");
        }

        // GET: Ocurrence/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ocurrences == null)
            {
                return NotFound();
            }

            var ocurrence = await _context.ocurrences
                .FirstOrDefaultAsync(m => m.OcurrenceId == id);
            if (ocurrence == null)
            {
                return NotFound();
            }

            return View(ocurrence);
        }

        // GET: Ocurrence/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ocurrence/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OcurrenceId,Locale,Status,Date,Description,Answer,Title")] Ocurrence ocurrence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocurrence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ocurrence);
        }

        // GET: Ocurrence/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ocurrences == null)
            {
                return NotFound();
            }

            var ocurrence = await _context.ocurrences.FindAsync(id);
            if (ocurrence == null)
            {
                return NotFound();
            }
            return View(ocurrence);
        }

        // POST: Ocurrence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OcurrenceId,Locale,Status,Date,Description,Answer,Title")] Ocurrence ocurrence)
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
            return View(ocurrence);
        }

        // GET: Ocurrence/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ocurrences == null)
            {
                return NotFound();
            }

            var ocurrence = await _context.ocurrences
                .FirstOrDefaultAsync(m => m.OcurrenceId == id);
            if (ocurrence == null)
            {
                return NotFound();
            }

            return View(ocurrence);
        }

        // POST: Ocurrence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ocurrences == null)
            {
                return Problem("Entity set 'QAccessContext.ocurrences'  is null.");
            }
            var ocurrence = await _context.ocurrences.FindAsync(id);
            if (ocurrence != null)
            {
                _context.ocurrences.Remove(ocurrence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcurrenceExists(string id)
        {
          return (_context.ocurrences?.Any(e => e.OcurrenceId == id)).GetValueOrDefault();
        }
    }
}
