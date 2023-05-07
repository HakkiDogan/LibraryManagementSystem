using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class WriterController : Controller
	{
		IWriterService _writerService;

		public WriterController(IWriterService writerService)
		{
			_writerService = writerService;
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
	}
}
