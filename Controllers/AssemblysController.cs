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
    public class AssemblysController : Controller
    {
        private readonly QAccessContext _context;

        public AssemblysController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Assemblys
        public async Task<IActionResult> Index()
        {
              return _context.Assemblies != null ? 
                          View(await _context.Assemblies.ToListAsync()) :
                          Problem("Entity set 'QAccessContext.Assemblies'  is null.");
        }

        // GET: Assemblys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assemblies == null)
            {
                return NotFound();
            }

            var @assembly = await _context.Assemblies
                .FirstOrDefaultAsync(m => m.AssemblyId == id);
            if (@assembly == null)
            {
                return NotFound();
            }

            return View(@assembly);
        }

        // GET: Assemblys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assemblys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssemblyId,Theme,Local,Link,Data,Minutes,CreateDate")] Assembly @assembly)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@assembly);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@assembly);
        }

        // GET: Assemblys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Assemblies == null)
            {
                return NotFound();
            }

            var @assembly = await _context.Assemblies.FindAsync(id);
            if (@assembly == null)
            {
                return NotFound();
            }
            return View(@assembly);
        }

        // POST: Assemblys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssemblyId,Theme,Local,Link,Data,Minutes,CreateDate")] Assembly @assembly)
        {
            if (id != @assembly.AssemblyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@assembly);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssemblyExists(@assembly.AssemblyId))
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
            return View(@assembly);
        }

        // GET: Assemblys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assemblies == null)
            {
                return NotFound();
            }

            var @assembly = await _context.Assemblies
                .FirstOrDefaultAsync(m => m.AssemblyId == id);
            if (@assembly == null)
            {
                return NotFound();
            }

            return View(@assembly);
        }

        // POST: Assemblys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assemblies == null)
            {
                return Problem("Entity set 'QAccessContext.Assemblies'  is null.");
            }
            var @assembly = await _context.Assemblies.FindAsync(id);
            if (@assembly != null)
            {
                _context.Assemblies.Remove(@assembly);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssemblyExists(int id)
        {
          return (_context.Assemblies?.Any(e => e.AssemblyId == id)).GetValueOrDefault();
        }
    }
}
