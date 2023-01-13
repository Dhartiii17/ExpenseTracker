using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
            
        }

        public DbSet<ExpensesDetails> Expenses { get; set; }
        public DbSet<CategoryDetails> Category { get; set; }
    }
}
