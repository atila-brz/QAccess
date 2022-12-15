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
        public async Task<IActionResult> Index(string? query, string? messageAlert, string? messageSuccess)
        {

            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "Name");
            
            if(String.Equals(query, Convert.ToString(Occurrence.StatusOccurrence.InProgress), StringComparison.OrdinalIgnoreCase))
            {
                var qAccessContext = _context.Occurrences.Where(o => o.Status == Occurrence.StatusOccurrence.InProgress)
                .Include(o => o.Responsable)
                .Include(o => o.ResponsibleOfficial);
                return View(await qAccessContext.ToListAsync());
            }
            else if(String.Equals(query, Convert.ToString(Occurrence.StatusOccurrence.Closed), StringComparison.OrdinalIgnoreCase))
            {
                var qAccessContext = _context.Occurrences.Where(o => o.Status == Occurrence.StatusOccurrence.Closed)
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
        public async Task<IActionResult> Details(int? id, string? messageAlert, string? messageSuccess)
        {
            if (id == null || _context.Occurrences == null)
            {
                return NotFound();
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            ViewData["ResponsibleOfficial"] = new SelectList(_context.Employees, "EmployeeId", "Name");

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
                return RedirectToAction(nameof(Index),  new { messageSuccess = "Ocorrência registrada com sucesso!"});
            }
            return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível registrar uma nova ocorrência!"});
        }

        // GET: Occurrences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Occurrences == null)
            {
                return NotFound();
            }

            var occurrence = await _context.Occurrences.FindAsync(id);

            if(!String.Equals(occurrence.Status, Occurrence.StatusOccurrence.Open))
            {
                return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId});
            }
            
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
                    return NotFound();
                }
                return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId, messageSuccess = "Os dados da ocorrência foram atualizados!"});
            }
            
            ViewData["messageAlert"] =  "Não foi possível atualizar os dados da ocorrência!";
            return View(occurrence);
        }

        // POST: Occurrences/Select/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Select(int id, int employeeId = 10)
        {
            Occurrence occurrence = await _context.Occurrences.FindAsync(id);
            
            if(await _context.Employees.FindAsync(employeeId) == null)
            {
                return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível registrar uma nova ocorrência!"});
            }

            occurrence.selectedForEmployee(employeeId);

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
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId, messageSuccess = "O status da ocorrência foi atualizado!"});
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
                    return NotFound();
                }
                return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId, messageSuccess = "O status da ocorrência foi atualizado!"});
            }
            return RedirectToAction(nameof(Details), new { id = occurrence.OccurrenceId, messageAlert = "Não foi possível atualizar o status da ocorrência!"});
        }


        // POST: Occurrences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Occurrences == null)
            {
                return NotFound();
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
