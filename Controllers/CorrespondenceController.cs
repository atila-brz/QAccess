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
    public class CorrespondenceController : Controller
    {
        private readonly QAccessContext _context;

        public CorrespondenceController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Correspondence
        public async Task<IActionResult> Index()
        {
              return _context.correspondences != null ? 
                          View(await _context.correspondences.ToListAsync()) :
                          Problem("Entity set 'QAccessContext.correspondences'  is null.");
        }

        // GET: Correspondence/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.correspondences
                .FirstOrDefaultAsync(m => m.CorrespondenceId == id);
            if (correspondence == null)
            {
                return NotFound();
            }

            return View(correspondence);
        }

        // GET: Correspondence/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Correspondence/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorrespondenceId,TrackingCode,Status,Sender,DateDelivery,DateWithdrawal,ResponsibleWithdrawal")] Correspondence correspondence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correspondence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(correspondence);
        }

        // GET: Correspondence/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.correspondences.FindAsync(id);
            if (correspondence == null)
            {
                return NotFound();
            }
            return View(correspondence);
        }

        // POST: Correspondence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CorrespondenceId,TrackingCode,Status,Sender,DateDelivery,DateWithdrawal,ResponsibleWithdrawal")] Correspondence correspondence)
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
            return View(correspondence);
        }

        // GET: Correspondence/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.correspondences
                .FirstOrDefaultAsync(m => m.CorrespondenceId == id);
            if (correspondence == null)
            {
                return NotFound();
            }

            return View(correspondence);
        }

        // POST: Correspondence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.correspondences == null)
            {
                return Problem("Entity set 'QAccessContext.correspondences'  is null.");
            }
            var correspondence = await _context.correspondences.FindAsync(id);
            if (correspondence != null)
            {
                _context.correspondences.Remove(correspondence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrespondenceExists(string id)
        {
          return (_context.correspondences?.Any(e => e.CorrespondenceId == id)).GetValueOrDefault();
        }
    }
}
