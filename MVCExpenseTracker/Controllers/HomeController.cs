using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Models;
using System.Diagnostics;

namespace MVCExpenseTracker.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MVCExpenseTrackerDbContext _context;

		public HomeController(ILogger<HomeController> logger, MVCExpenseTrackerDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Expenses()
		{
			//create list out of the Expenses table from our db and put the list into the view
			var allExpenses = _context.Expenses.ToList();

			var totalExpenses = allExpenses.Sum(x => x.Value);
			ViewBag.TotalExpenses = totalExpenses;

			return View(allExpenses);
		}

		public IActionResult CreateEditExpense(int? id) //nulleable because id could be null when expense is new
		{
			if (id != null)
			{
				//editing -> load an expense by id
				var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
				return View(expenseInDb);
			}

			return View();
		}

		public IActionResult DeleteExpense(int id)
		{
			//searching for item in db with fitting id to delete
			//take first one/expense found that matches Id
			var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
			_context.Expenses.Remove(expenseInDb);
			_context.SaveChanges();

			return RedirectToAction("Expenses");
		}

		public IActionResult CreateEditExpenseForm(Expense model)
		{
			if (model.Id == 0)
			{
				//adding model to the Expenses table of our database(_context, see above) & save changes
				_context.Expenses.Add(model);
			}
			else
			{
				//editing exisitng data/ expense
				_context.Expenses.Update(model);
			}

			_context.SaveChanges();

			//redirect to Index view
			return RedirectToAction("Expenses");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
