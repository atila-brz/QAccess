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
    public class CorrespondencesController : Controller
    {
        private readonly QAccessContext _context;

        public CorrespondencesController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Correspondences
        public async Task<IActionResult> Index(string? messageAlert, string? messageSuccess)
        {
            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }
            
            ViewData["EmployeeWithdrawalId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block");
            
            var qAccessContext = _context.Correspondences.Include(c => c.EmployeeDelivery).Include(c => c.Unit);
            return View(await qAccessContext.ToListAsync());
        }

        // GET: Correspondences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Correspondences == null)
            {
                return NotFound();
            }

            ViewData["EmployeeWithdrawalId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            var correspondence = await _context.Correspondences
                .Include(c => c.EmployeeDelivery)
                .Include(c => c.EmployeeWithdrawal)
                .Include(c => c.Unit)
                .FirstOrDefaultAsync(m => m.CorrespondenceId == id);
                
            if (correspondence == null)
            {
                return NotFound();
            }

            return View(correspondence);
        }

        // POST: Correspondences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorrespondenceId,TrackingCode,Sender,UnitId,DateDelivery,EmployeeDeliveryId,EmployeeWithdrawalId")] Correspondence correspondence)
        {   
            correspondence.DateDelivery = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                _context.Add(correspondence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { messageSuccess = "Correspondência registrada!"});
            }

            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", correspondence.EmployeeDeliveryId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block", correspondence.UnitId);
            return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível registrar a correspondência!"});
        }

        // GET: Correspondences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Correspondences == null)
            {
                return NotFound();
            }

            var correspondence = await _context.Correspondences
                .Include(c => c.EmployeeDelivery)
                .Include(c => c.EmployeeWithdrawal)
                .Include(c => c.Unit)
                .FirstOrDefaultAsync(m => m.CorrespondenceId == id);

            if(!correspondence.isAvailable())
            {
                return RedirectToAction(nameof(Details), new {id = correspondence.CorrespondenceId});
            }
                
            if (correspondence == null)
            {
                return NotFound();
            }
            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", correspondence.EmployeeDeliveryId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block", correspondence.UnitId);
            return View(correspondence);
        }

        // POST: Correspondences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CorrespondenceId,TrackingCode,Status,Sender,UnitId,DateDelivery,EmployeeDeliveryId,EmployeeWithdrawalId")] Correspondence correspondence)
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
                    return NotFound();
                }

                return RedirectToAction(nameof(Details));
            }

            ViewData["EmployeeDeliveryId"] = new SelectList(_context.Employees, "EmployeeId", "ContactNumber", correspondence.EmployeeDeliveryId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "Block", correspondence.UnitId);
            return View(correspondence);
        }

        // POST: Correspondences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Correspondences == null)
            {
                return Problem("Entity set 'QAccessContext.Correspondences'  is null.");
            }
            var correspondence = await _context.Correspondences.FindAsync(id);
            if (correspondence != null)
            {
                if(correspondence.isAvailable())
                {
                    _context.Correspondences.Remove(correspondence);
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delivery(int id, int employeeWithdrawalId, string responsibleWithdrawal)
        {
            if(!CorrespondenceExists(id))
            {
                return RedirectToAction(nameof(Index), new {messageAlert = "Não foi possível atualizar a correspondência!"});
            }
            else
            {
                var correspondence = await _context.Correspondences.FindAsync(id);

                if(correspondence.DeliveryCorrespondence(employeeWithdrawalId, responsibleWithdrawal)){
                    try
                    {
                        _context.Update(correspondence);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), new {messageSuccess = "Correspondência atualizada!"});
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return RedirectToAction(nameof(Index), new {messageAlert = "Não foi possível atualizar a correspondência!"});
                    }
                }
            } 

            return RedirectToAction(nameof(Index), new {messageAlert = "Não foi possível atualizar a correspondência!"});
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }

        private bool CorrespondenceExists(int id)
        {
          return (_context.Correspondences?.Any(e => e.CorrespondenceId == id)).GetValueOrDefault();
        }
    }
}
