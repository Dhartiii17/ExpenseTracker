using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class ExpensesDetails
    {
        [Key]
        public int ExpensesId { get; set; }

        // forgien Key
        [Range(1,int.MaxValue,ErrorMessage = "Please Select a category")]
        public int CategoryId { get; set; }
        public CategoryDetails? Category { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
