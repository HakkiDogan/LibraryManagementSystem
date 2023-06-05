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

		//void RemoveBook(int id);

		//void UpdateBook(Book book);

		BookTransaction GetById(int id);
	}
}
