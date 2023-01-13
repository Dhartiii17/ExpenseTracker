using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Linq;


namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            //List of Expenses
            List<ExpensesDetails> SelectedExpenses = await _context.Expenses
                .Include(x => x.Category).ToListAsync();
            ViewBag.Expenses = SelectedExpenses;

            //List of Category
            List<CategoryDetails> SelectCategory = await _context.Category.ToListAsync();


            //TotalExpenses 
            int TotalExpenses = SelectedExpenses.Sum(j => j.Amount);
            ViewBag.TotalExpenses = TotalExpenses;

            //TotalExpensesLimit
            int TotalExpensesLimit = SelectCategory.Sum(i => i.ExpenseLimet);
            ViewBag.TotalExpensesLimit = TotalExpensesLimit;

            //List of CategoryName 
            List<CategoryDetails> CategoryName = await _context.Category.ToListAsync();
            ViewBag.Category = CategoryName;

            return View();

            

        }

       public async Task<IActionResult> ExpensesList(int? id)
        {
            if(id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var ExpensesDetail = await _context.Expenses.Include(e => e.Category).Where(f => f.CategoryId == id).ToListAsync();
            
            if(ExpensesDetail == null)
            {
                return NotFound();
            }

            // select Category
            List<CategoryDetails> SelectCategory = await _context.Category.Where(m => m.CategoryId == id).ToListAsync();
            ViewBag.CategoryLimit = SelectCategory;

            //select Expenses
            List<ExpensesDetails> SelectExpenses = await _context.Expenses.Include(n => n.Category).ToListAsync();

            int TotalCategory = SelectExpenses.Where(j => j.CategoryId == id).Sum(k => k.Amount);
            ViewBag.CategoryExpenses = TotalCategory;

            int Categorylimit = SelectCategory.Where(y => y.CategoryId == id).Sum(m => m.ExpenseLimet);
            ViewBag.CategoryLimit = Categorylimit;

            return View(ExpensesDetail);



        }

        
     
        
    }
}
