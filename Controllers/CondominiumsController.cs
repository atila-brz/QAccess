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
    public class CondominiumsController : Controller
    {
        private readonly QAccessContext _context;

        public CondominiumsController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Condominiums
        public async Task<IActionResult> Index(string? messageAlert, string? messageSuccess)
        {   
            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            return _context.Codominiums != null ? 
                        View(await _context.Codominiums.ToListAsync()) :
                        NotFound();
        }

        // GET: Condominiums/Details/5
        public async Task<IActionResult> Details(int? id, string? messageSuccess)
        {
            if (id == null || _context.Codominiums == null)
            {
                return NotFound();
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            var condominium = await _context.Codominiums
                .FirstOrDefaultAsync(m => m.CondominiumId == id);

            if (condominium == null)
            {
                return NotFound();
            }

            return View(condominium);
        }

        // POST: Condominiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CondominiumId,Name,Email,Gender,BirthDate,MaritalStatus,ContactNumber,Cpf")] Condominium condominium)
        {
            if (ModelState.IsValid)
            {
                if(condominium.MaritalStatusIsValid(condominium.MaritalStatus) is not false && condominium.GenderIsValid(condominium.Gender) is not false)
                {
                    _context.Add(condominium);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { messageSuccess = "Novo condômino registrado!"});
                }   
                
                return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível adicionar um novo condômino!"});
            
            }
            return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível adicionar um novo condômino!"});
        }

        // GET: Condominiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Codominiums == null)
            {
                return NotFound();
            }

            var condominium = await _context.Codominiums.FindAsync(id);

            if (condominium == null)
            {
                return NotFound();
            }

            return View(condominium);
        }

        // POST: Condominiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CondominiumId,Name,Email,Gender,BirthDate,MaritalStatus,ContactNumber,Cpf")] Condominium condominium)
        {
            if (id != condominium.CondominiumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(condominium.MaritalStatusIsValid(condominium.MaritalStatus) is not false && condominium.GenderIsValid(condominium.Gender) is not false)
                    {
                        _context.Update(condominium);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Details), new { id = condominium.CondominiumId, messageSuccess = "Os dados do condômino foram atualizados!"});
                    }

                    return View(condominium);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }

            ViewData["messageAlert"] =  "Não foi possível atualizar os dados do condômino!";
            return View(condominium);
        }

        // POST: Condominiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Codominiums == null)
            {
                return NotFound();
            }
            var condominium = await _context.Codominiums.FindAsync(id);

            if (condominium != null)
            {
                _context.Codominiums.Remove(condominium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CondominiumExists(int id)
        {
          return (_context.Codominiums?.Any(e => e.CondominiumId == id)).GetValueOrDefault();
        }
    }
}
