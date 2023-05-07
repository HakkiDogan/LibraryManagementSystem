using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IBookService 
	{
		List<Book> GetAll();

		void AddBook(Book book);

		void RemoveBook(int id);

		void UpdateBook(Book book);

		Book GetById(int id);
	}
}
