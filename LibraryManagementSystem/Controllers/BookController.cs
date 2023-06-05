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

		public IActionResult Index(string p)
		{
			var books = from b in _bookService.GetAll() select b;
			if (!string.IsNullOrEmpty(p))
			{
				books = books.Where(b => b.Name.Contains(p));
			}
			return View(books.ToList());
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
			
			_bookService.AddBook(book);
			return RedirectToAction("Index");
		}

		public IActionResult RemoveBook(int id)
		{
			_bookService.RemoveBook(id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UpdateBook(int id)
		{
			var book = _bookService.GetById(id);
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
			return View("UpdateBook", book);
		}

		[HttpPost]
		public IActionResult UpdateBook(Book book)
		{
			_bookService.UpdateBook(book);
			return RedirectToAction("Index");
		}
	}
}
