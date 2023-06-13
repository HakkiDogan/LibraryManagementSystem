using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
		IPunishmentDal _punishmentDal;

		public BookTransactionManager(IBookTransactionDal bookTransactionDal, IMemberService memberService, IStaffService staffService, IBookService bookService, IPunishmentDal punishmentDal)
		{
			_bookTransactionDal = bookTransactionDal;
			_memberService = memberService;
			_staffService = staffService;
			_bookService = bookService;
			_punishmentDal = punishmentDal;
		}

		public void AddBookTransaction(BookTransaction bookTransaction)
		{
			bookTransaction.Member = _memberService.GetById(bookTransaction.MemberId);
			bookTransaction.Staff = _staffService.GetById(bookTransaction.StaffId);
			bookTransaction.Book = _bookService.GetById(bookTransaction.BookId);
			//Kitap birine verildiği için durumu false yapılıyor.
			bookTransaction.Book.IsStatus = false;
			_bookService.UpdateBook(bookTransaction.Book);
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
			var bookTransaction = _bookTransactionDal.Get(b => b.BookTransactionId == id);
			bookTransaction.Member = _memberService.GetById(bookTransaction.MemberId);
			bookTransaction.Staff = _staffService.GetById(bookTransaction.StaffId);
			bookTransaction.Book = _bookService.GetById(bookTransaction.BookId);
			return bookTransaction;
		}

		public void BookReturn(BookTransaction bookTransaction, decimal punishmentPrice)
		{
			var _bookTransaction = GetById(bookTransaction.BookTransactionId);
			bookTransaction.MemberId = _bookTransaction.MemberId;
			bookTransaction.StaffId = _bookTransaction.StaffId;
			bookTransaction.BookId = _bookTransaction.BookId;
			bookTransaction.TransactionStatus = true;
			_bookTransactionDal.Update(bookTransaction);
			if (punishmentPrice > 0)
			{
				_punishmentDal.Add(new Punishment
				{
					BookTransactionId= bookTransaction.BookTransactionId,
					Money= punishmentPrice,
					MemberId = bookTransaction.MemberId,
					StartDate = (DateTime)bookTransaction.MemberReturnDate,
				});
			}
		
			var book = _bookService.GetById(bookTransaction.BookId);
			book.IsStatus= true;
			_bookService.UpdateBook(book);
		}

		public List<TransactionHistoryModel> TransactionHistories()
		{
			List<TransactionHistoryModel> transactionHistories = new List<TransactionHistoryModel>();
			var bookTransactions = GetAll().Where(bt => bt.TransactionStatus == true).ToList();
			var punishments = _punishmentDal.GetAll();
			bookTransactions.ForEach(bt =>
			{
				transactionHistories.Add(new TransactionHistoryModel()
				{
					BookTransactionId = bt.BookTransactionId,
					MemberId = bt.MemberId,
					BookName = bt.Book.Name,
					MemberName = bt.Member.Name + " " +bt.Member.Surname,
					StaffName= bt.Staff.Name,
					IssueDate= bt.IssueDate,
					MemberReturnDate= bt.MemberReturnDate,
					Money = _punishmentDal.Get(p => p.BookTransactionId == bt.BookTransactionId)?.Money  != null ? _punishmentDal.Get(p => p.BookTransactionId == bt.BookTransactionId).Money : 0
				});
			});

			return transactionHistories;
		}

		public List<TransactionHistoryModel> MyBooks(int memberId)
		{
			var myTransacationHistory = TransactionHistories().Where(th => th.MemberId == memberId).ToList();
			return myTransacationHistory;
		}

	}
}
