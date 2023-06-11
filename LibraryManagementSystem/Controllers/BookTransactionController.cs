using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
	public class BookTransactionController : Controller
	{
		IBookTransactionService _bookTransactionService;
		IMemberService _memberService;
		IStaffService _staffService;
		IBookService _bookService;

		public BookTransactionController(IMemberService memberService, IStaffService staffService, IBookService bookService, IBookTransactionService bookTransactionService)
		{
			_memberService = memberService;
			_staffService = staffService;
			_bookService = bookService;
			_bookTransactionService = bookTransactionService;
		}

		public IActionResult Index()
		{
			var bookTransaction = _bookTransactionService.GetAll().Where(bt => bt.TransactionStatus == false).ToList();
			return View(bookTransaction);
		}

		//Ödünç kitap verme
		[HttpGet]
		public IActionResult LendingBook()
		{
			List<SelectListItem> members = (from m in _memberService.GetAll().ToList()
											select new SelectListItem
											{
												Text = m.Name + " " + m.Surname,
												Value = m.MemberId.ToString()
											}).ToList();

			ViewBag.Members = members;

			List<SelectListItem> staffs = (from s in _staffService.GetAll().ToList()
										   select new SelectListItem
										   {
											   Text = s.Name,
											   Value = s.StaffId.ToString()
										   }).ToList();
			ViewBag.Staffs = staffs;

			List<SelectListItem> books = (from b in _bookService.GetAll().Where(b => b.IsStatus == true).ToList()
										  select new SelectListItem
										  {
											  Text = b.Name,
											  Value = b.BookId.ToString()
										  }).ToList();
			ViewBag.Books = books;

			return View();
		}

		[HttpPost]
		public IActionResult LendingBook(BookTransaction bookTransaction)
		{
			_bookTransactionService.AddBookTransaction(bookTransaction);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult BookReturn(int id)
		{
			var bookTransaction = _bookTransactionService.GetById(id);
			DateTime d1 = DateTime.Parse(bookTransaction.ReturnDate.ToString());
			DateTime d2 = DateTime.Now.Date;
			TimeSpan difference = d2 - d1;
			int daysDifference = difference.Days;
			ViewBag.PunishmentPrice = (daysDifference*5);
			TempData["price"]  = daysDifference*5;
			return View(bookTransaction);
		}

		[HttpPost]
		public IActionResult BookReturn(BookTransaction bookTransaction)
		{
			_bookTransactionService.BookReturn(bookTransaction, Convert.ToDecimal(TempData["price"]));
			return RedirectToAction("Index");
		}

		public IActionResult Transactions()
		{
			var bookTransaction = _bookTransactionService.GetAll().Where(bt => bt.TransactionStatus == true).ToList();
			return View(bookTransaction);
		}
	}
}
