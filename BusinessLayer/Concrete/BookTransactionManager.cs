using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class BookTransactionManager : IBookTransactionService
	{
		IBookTransactionDal _bookTransactionDal;
		IMemberService _memberService;
		IStaffService _staffService;
		IBookService _bookService;

		public BookTransactionManager(IBookTransactionDal bookTransactionDal, IMemberService memberService, IStaffService staffService, IBookService bookService)
		{
			_bookTransactionDal = bookTransactionDal;
			_memberService = memberService;
			_staffService = staffService;
			_bookService = bookService;
		}

		public void AddBookTransaction(BookTransaction bookTransaction)
		{
			bookTransaction.Member = _memberService.GetById(bookTransaction.MemberId);
			bookTransaction.Staff = _staffService.GetById(bookTransaction.StaffId);
			bookTransaction.Book = _bookService.GetById(bookTransaction.BookId);
			_bookTransactionDal.Add(bookTransaction);
		}

		public List<BookTransaction> GetAll()
		{
			var bookTransactions = _bookTransactionDal.GetAll().ToList();
			bookTransactions.ForEach(b =>
			{
				b.Member = _memberService.GetById(b.MemberId);
				b.Staff = _staffService.GetById(b.StaffId);
				b.Book = _bookService.GetById(b.BookId);
			});
			return bookTransactions;
		}

		public BookTransaction GetById(int id)
		{
			var bookTransaction = _bookTransactionDal.Get(b => b.BookId == id);
			bookTransaction.Member = _memberService.GetById(bookTransaction.MemberId);
			bookTransaction.Staff = _staffService.GetById(bookTransaction.StaffId);
			bookTransaction.Book = _bookService.GetById(bookTransaction.BookId);
			return bookTransaction;
		}
	}
}
