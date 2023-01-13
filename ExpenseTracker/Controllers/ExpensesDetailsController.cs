using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExpenseTracker.Controllers
{
    public class ExpensesDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExpensesDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Expenses.Include(e => e.Category);
            return View(await applicationDbContext.ToListAsync());
        }

       

        // GET: ExpensesDetails/AddOREdit
        public IActionResult AddOrEdit(int id = 0)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            if (id == 0)
                return View(new ExpensesDetails());
            else
                return View(_context.Expenses.Find(id));

        }

        // POST: ExpensesDetails/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("ExpensesId,CategoryId,Title,Description,Amount,Date")] ExpensesDetails expensesDetails)
        {
            if (ModelState.IsValid)
            {
                if (expensesDetails.ExpensesId == 0)
                    _context.Add(expensesDetails);
                else
                    _context.Update(expensesDetails);

                
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", expensesDetails.CategoryId);
            return View(expensesDetails);
        }

       

       

        // POST: ExpensesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expenses'  is null.");
            }
            var expensesDetails = await _context.Expenses.FindAsync(id);
            if (expensesDetails != null)
            {
                _context.Expenses.Remove(expensesDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
