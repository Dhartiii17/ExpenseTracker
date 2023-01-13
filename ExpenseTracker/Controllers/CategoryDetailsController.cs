using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker.Controllers
{
    public class CategoryDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.Category.ToListAsync());
        }

        // GET: CategoryDetails/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new CategoryDetails());
            else
                return View(_context.Category.Find(id));
        }

        // POST: CategoryDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Name,ExpenseLimet")] CategoryDetails categoryDetails)
        {
            if (ModelState.IsValid)
            {
                if (categoryDetails.CategoryId == 0)
                    _context.Add(categoryDetails);
                else
                    _context.Update(categoryDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDetails);
        }


        

        // POST: CategoryDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Category'  is null.");
            }
            var categoryDetails = await _context.Category.FindAsync(id);
            if (categoryDetails != null)
            {
                _context.Category.Remove(categoryDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
