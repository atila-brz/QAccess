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
    public class PublicationsController : Controller
    {
        private readonly QAccessContext _context;

        public PublicationsController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Publications
        public async Task<IActionResult> Index()
        {
            var qAccessContext = _context.Publications.Include(p => p.Creator);
            return View(await qAccessContext.ToListAsync());
        }

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publications == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(m => m.PublicationId == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // // GET: Publications/Create
        // public IActionResult Create()
        // {
        //     ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber");
        //     return View();
        // }

        // POST: Publications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicationId,Title,Description,Link,ContactNumber,Type,Photo")] Publication publication)
        {
            publication.CreateDate = DateTime.Now;
            publication.IsActive = false;
            publication.Views = 0;
            publication.UpdateDate = publication.CreateDate;
            publication.CondominiumId = 0;// TODO: Change this to the logged user's condominium
            if (ModelState.IsValid)
            {
                _context.Add(publication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publication);
        }

        // GET: Publications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publications == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber", publication.CondominiumId);
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicationId,Title,Description,Link,CondominiumId,ContactNumber,Type,CreateDate,IsActive,UpdateDate,Views,Photo")] Publication publication)
        {
            if (id != publication.PublicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(publication.PublicationId))
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
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "ContactNumber", publication.CondominiumId);
            return View(publication);
        }

        // // GET: Publications/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null || _context.Publications == null)
        //     {
        //         return NotFound();
        //     }

        //     var publication = await _context.Publications
        //         .Include(p => p.Creator)
        //         .FirstOrDefaultAsync(m => m.PublicationId == id);
        //     if (publication == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(publication);
        // }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publications == null)
            {
                return Problem("Entity set 'QAccessContext.Publications'  is null.");
            }
            var publication = await _context.Publications.FindAsync(id);
            if (publication != null)
            {
                _context.Publications.Remove(publication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationExists(int id)
        {
            return (_context.Publications?.Any(e => e.PublicationId == id)).GetValueOrDefault();
        }
    }
}
