using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
	public class BookController : Controller
	{
		IBookService _bookService;
		ICategoryService _categoryService;
		IWriterService _writerService;
		IPublisherService _publisherService;

		public BookController(IBookService bookService , ICategoryService categoryService, IWriterService writerService, IPublisherService publisherService)
		{
			_bookService = bookService;
			_categoryService = categoryService;
			_writerService = writerService;
			_publisherService = publisherService;
		}

		public IActionResult Index()
		{
			var books = _bookService.GetAll();
			return View(books);
		}

		[HttpGet]
		public IActionResult AddBook()
		{
			List<SelectListItem> categories = (from c in _categoryService.GetAll().ToList()
										   select new SelectListItem
										   {
											   Text = c.Name,
											   Value = c.CategoryId.ToString()
										   }).ToList();
			ViewBag.Categories = categories;

			List<SelectListItem> writers = (from w in _writerService.GetAll().ToList()
											   select new SelectListItem
											   {
												   Text = w.Name + " " + w.Surname,
												   Value = w.WriterId.ToString()
											   }).ToList();
			ViewBag.Writers = writers;

			List<SelectListItem> publishers = (from p in _publisherService.GetAll().ToList()
											select new SelectListItem
											{
												Text = p.Name,
												Value = p.PublisherId.ToString()
											}).ToList();
			ViewBag.Publishers = publishers;
			return View();
		}

		[HttpPost]
		public IActionResult AddBook(Book book)
		{
			var category = _categoryService.GetById(book.CategoryId);
			var writer = _writerService.GetById(book.WriterId);
			var publisher = _publisherService.GetById(book.PublisherId);
			book.Category =	category;
			book.Writer = writer;
			book.Publisher = publisher;
			_bookService.AddBook(book);
			return RedirectToAction("Index");
		}
	}
}
