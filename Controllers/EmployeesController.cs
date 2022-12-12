using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QAccess.Models;

namespace QAccess
{
    public class EmployeesController : Controller
    {
        private readonly QAccessContext _context;

        public EmployeesController(QAccessContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string? messageAlert, string? messageSuccess)
        {
            if(messageAlert is not null){
                ViewData["messageAlert"] =  messageAlert;
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            return _context.Employees != null ? 
                    View(await _context.Employees.ToListAsync()) :
                    NotFound();
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id, string? messageSuccess)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            if(messageSuccess is not null){
                ViewData["messageSuccess"] =  messageSuccess;
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Email,Gender,Cpf,BirthDate,MaritalStatus,ContactNumber,Office,Sector")] Employee employee)
        {   

            if (ModelState.IsValid) 
            {                
                if(employee.MaritalStatusIsValid(employee.MaritalStatus) is not false && employee.GenderIsValid(employee.Gender) is not false){
                    
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { messageSuccess = "Novo funcionário registrado!"});
                }

                return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível adicionar um novo funcionário!"});
            }
            return RedirectToAction(nameof(Index), new { messageAlert = "Não foi possível adicionar um novo funcionário!"});
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Email,Gender,Cpf,BirthDate,MaritalStatus,ContactNumber,Office,Sector")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(employee.MaritalStatusIsValid(employee.MaritalStatus) is not false && employee.GenderIsValid(employee.Gender) is not false){
                       
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Details), new { id = employee.EmployeeId, messageSuccess = "Os dados do funcionário foram atualizados!"});
                    }
                    
                    ViewData["messageAlert"] =  "Não foi possível atualizar os dados do funcionário!";
                    return View(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }

            ViewData["messageAlert"] =  "Não foi possível atualizar os dados do funcionário!";
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
