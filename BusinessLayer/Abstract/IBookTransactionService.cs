using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IBookTransactionService
	{
		List<BookTransaction> GetAll();

		void AddBookTransaction(BookTransaction book);
		List<BookTransaction> MyBooks(int memberId);
		void BookReturn(BookTransaction bookTransaction, decimal punishmentPrice);
		BookTransaction GetById(int id);
	}
}
