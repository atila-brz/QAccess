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
    public class UnitController : Controller
    {
        private readonly QAccessContext _context;

        public UnitController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Unit
        public async Task<IActionResult> Index()
        {
              return _context.units != null ? 
                          View(await _context.units.ToListAsync()) :
                          Problem("Entity set 'QAccessContext.units'  is null.");
        }

        // GET: Unit/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.units == null)
            {
                return NotFound();
            }

            var unit = await _context.units
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Unit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,Tower,Block,Number,Country,State,City,Road,Complement,FkCondominium")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Unit/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.units == null)
            {
                return NotFound();
            }

            var unit = await _context.units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UnitId,Tower,Block,Number,Country,State,City,Road,Complement,FkCondominium")] Unit unit)
        {
            if (id != unit.UnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.UnitId))
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
            return View(unit);
        }

        // GET: Unit/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.units == null)
            {
                return NotFound();
            }

            var unit = await _context.units
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.units == null)
            {
                return Problem("Entity set 'QAccessContext.units'  is null.");
            }
            var unit = await _context.units.FindAsync(id);
            if (unit != null)
            {
                _context.units.Remove(unit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(string id)
        {
          return (_context.units?.Any(e => e.UnitId == id)).GetValueOrDefault();
        }
    }
}
