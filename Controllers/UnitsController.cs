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
    public class UnitsController : Controller
    {
        private readonly QAccessContext _context;

        public UnitsController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Units
        public async Task<IActionResult> Index(string? messageAlert, string? messageSuccess)
        {
            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            return _context.Units != null ? 
                        View(await _context.Units.ToListAsync()) :
                        NotFound();
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id, string? messageSuccess)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            var unit = await _context.Units
                .FirstOrDefaultAsync(m => m.UnitId == id);

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,Tower,Block,Number,Country,State,City,Road,Complement")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),  new { messageSuccess = "Unidade registrada com sucesso!"});
            }
            return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível registrar uma nova unidade!"});
        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitId,Tower,Block,Number,Country,State,City,Road,Complement")] Unit unit)
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
                        return NotFound();
                }
                return RedirectToAction(nameof(Details), new { id = unit.UnitId, messageSuccess = "Os dados da unidade foram atualizados!"});
            }

            ViewData["messageAlert"] =  "Não foi possível atualizar os dados da unidade!";
            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Units == null)
            {
                return NotFound();
            }
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(int id)
        {
          return (_context.Units?.Any(e => e.UnitId == id)).GetValueOrDefault();
        }
    }
}
