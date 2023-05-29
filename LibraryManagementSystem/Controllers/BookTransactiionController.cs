using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class BookTransactiionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		//Ödünç kitap verme
		public IActionResult LendingBook()
		{
			return View();
		}
	}
}
