using System.ComponentModel.DataAnnotations;

namespace MVCExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }

        //has default value
        public decimal Value { get; set; }
        
        [Required] //have to enter value for Description
        public string? Description { get; set; }
    }
}
