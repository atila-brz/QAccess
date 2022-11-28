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
    public class OccurrencesController : Controller
    {
        private readonly QAccessContext _context;

        public OccurrencesController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Occurrences
        public async Task<IActionResult> Index(string? query)
        {

            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "Name");
            if(String.Equals(query, Convert.ToString(Occurrence.StatusOccurrence.InProgress), StringComparison.OrdinalIgnoreCase))
            {
                var qAccessContext = _context.Occurrences.Where(o => o.Status == Occurrence.StatusOccurrence.InProgress && o.EmployeeId == 1)
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial);
                return View(await qAccessContext.ToListAsync());
            }
            else if(String.Equals(query, Convert.ToString(Occurrence.StatusOccurrence.Closed), StringComparison.OrdinalIgnoreCase))
            {
                var qAccessContext = _context.Occurrences.Where(o => o.Status == Occurrence.StatusOccurrence.Closed && o.EmployeeId == 1)
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial);;
                return View(await qAccessContext.ToListAsync());
            }
            else if(String.Equals(query, Convert.ToString(Occurrence.StatusOccurrence.Open), StringComparison.OrdinalIgnoreCase))
            {
                var qAccessContext = _context.Occurrences.Where(o => o.Status == Occurrence.StatusOccurrence.Open)
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial);;
                return View(await qAccessContext.ToListAsync());
            }
            else
            {
                var qAccessContext = _context.Occurrences
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial);
                return View(await qAccessContext.ToListAsync());
            }

        }

        // GET: Occurrences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Occurrences == null)
            {
                return NotFound();
            }

            var occurrence = await _context.Occurrences
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial)
                .FirstOrDefaultAsync(m => m.OccurrenceId == id);
            if (occurrence == null)
            {
                return NotFound();
            }

            return View(occurrence);
        }

        // GET: Occurrences/Create
        // public IActionResult Create()
        // {
        //     ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "Name");
        //     return View();
        // }

        // POST: Occurrences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OccurrenceId,Locale,CondominiumId,Description,Title,PhotoBase64")] Occurrence occurrence)
        {
            DateTime date = DateTime.Now;
            occurrence.CreationDate = date;
            occurrence.Status = Occurrence.StatusOccurrence.Open;
            if (ModelState.IsValid)
            {
                _context.Add(occurrence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "Name", occurrence.CondominiumId);
            return View(occurrence);
        }

        // GET: Occurrences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Occurrences == null)
            {
                return NotFound();
            }

            var occurrence = await _context.Occurrences.FindAsync(id);
            if (occurrence == null)
            {
                return NotFound();
            }
            Condominium condominium = _context.Codominiums.Find(occurrence.CondominiumId);
            ViewData["Responsable"] = condominium.Name;
            return View(occurrence);
        }

        // POST: Occurrences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OccurrenceId,CreationDate,Locale,Status,CondominiumId,Description,Title,PhotoBase64")] Occurrence occurrence)
        {
            if (id != occurrence.OccurrenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occurrence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccurrenceExists(occurrence.OccurrenceId))
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
            return View(occurrence);
        }

        // POST: Occurrences/Select/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Select(int id)
        {
            Occurrence occurrence = await _context.Occurrences.FindAsync(id);
            if(await _context.Employees.FindAsync(1)==null)
            {
                return NotFound();
            }
            occurrence.selectedForEmployee(1);

            if (id != occurrence.OccurrenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occurrence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccurrenceExists(occurrence.OccurrenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId});
            }
            return View(occurrence);
        }

        //post: Occurrences/Finish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int id, string answer )
        {
            Occurrence occurrence = await _context.Occurrences.FindAsync(id);
            occurrence.Answer = answer;
            occurrence.closeOccurrence();

            if (id != occurrence.OccurrenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occurrence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccurrenceExists(occurrence.OccurrenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId});
            }
            return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId});
        }


        // POST: Occurrences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Occurrences == null)
            {
                return Problem("Entity set 'QAccessContext.Occurrences'  is null.");
            }
            var occurrence = await _context.Occurrences.FindAsync(id);
            if (occurrence != null)
            {
                _context.Occurrences.Remove(occurrence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccurrenceExists(int id)
        {
            return (_context.Occurrences?.Any(e => e.OccurrenceId == id)).GetValueOrDefault();
        }
    }
}
