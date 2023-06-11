using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class WriterController : Controller
	{
		IWriterService _writerService;
		IBookService _bookService;

		public WriterController(IWriterService writerService, IBookService bookService)
		{
			_writerService = writerService;
			_bookService = bookService;
		}

		public IActionResult Index()
		{
			var writers = _writerService.GetAll();
			return View(writers);
		}

		[HttpGet]
		public IActionResult AddWriter()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddWriter(Writer writer) 
		{
			_writerService.AddWriter(writer);
			return RedirectToAction("Index");
        }

		public IActionResult RemoveWriter(int id)
		{
			_writerService.RemoveWriter(id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UpdateWriter(int id)
		{
			var writer = _writerService.GetById(id);
			return View("UpdateWriter",writer);
		}

		[HttpPost]
		public IActionResult UpdateWriter(Writer writer)
		{
			_writerService.UpdateWriter(writer);
			return RedirectToAction("Index");
		}

		public IActionResult WriterBooks(int id)
		{
			var writer = _writerService.GetById(id);
			ViewBag.WriterFullName = writer.Name + " " + writer.Surname;
			var books = _bookService.BooksByWriterId(id);
			return View(books);
		}


	}
}
