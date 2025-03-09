using Microsoft.EntityFrameworkCore;

namespace MVCExpenseTracker.Models
{
	public class MVCExpenseTrackerDbContext : DbContext
	{

        //expenses table in database
		public DbSet<Expense> Expenses { get; set; }

        public MVCExpenseTrackerDbContext(DbContextOptions<MVCExpenseTrackerDbContext> options)
            : base(options)
        {
            
        }
    }
}
