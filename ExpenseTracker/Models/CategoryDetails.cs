using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class CategoryDetails
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

       
        [Required(ErrorMessage = "ExpensesLimit is Required")]
        public int ExpenseLimet { get; set; }
    }
}
