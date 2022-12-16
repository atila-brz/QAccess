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
        public async Task<IActionResult> Index(string? messageAlert, string? messageSuccess)
        {
            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            ViewData["CondominiumId"] = new SelectList(_context.Codominiums, "CondominiumId", "Name");

            var qAccessContext = _context.Publications.Include(p => p.Creator);
            return View(await qAccessContext.ToListAsync());
        }

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id, string? messageSuccess)
        {
            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

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

            publication.Views++;
            _context.Update(publication);
            await _context.SaveChangesAsync();

            return View(publication);
        }

        // POST: Publications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CondominiumId, PublicationId,Title,Description,Link,ContactNumber,Type,Photo")] Publication publication)
        {
            publication.CreateDate = DateTime.Now;
            publication.IsActive = false;
            publication.Views = 0;
            publication.UpdateDate = publication.CreateDate;
            if (ModelState.IsValid)
            {
                _context.Add(publication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),  new { messageSuccess = "Anúncio registrado com sucesso!"});
            }
            return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível registrar um novo anúncio!"});
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
            ViewData["CondominiumId"] = await _context.Codominiums.FindAsync(publication.CondominiumId);
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicationId,Title,Description,Link,ContactNumber,Type,Photo,CondominiumId")] Publication publication)
        {
            if (id != publication.PublicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                publication.UpdateDate = DateTime.Now;
                try
                {
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Details), new { id = publication.PublicationId, messageSuccess = "Os dados do anúncio foram atualizados!"});
            }
            ViewData["CondominiumId"] = await _context.Codominiums.FindAsync(publication.CondominiumId);
            ViewData["messageAlert"] =  "Não foi possível atualizar os dados do anúncio";
            return View(publication);
        }


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
