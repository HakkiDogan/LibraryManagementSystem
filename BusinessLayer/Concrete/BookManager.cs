using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class BookManager : IBookService
	{
		IBookDal _bookDal;
		IWriterService _writerService;
		ICategoryService _categoryService;
		IPublisherService _publisherService;

		public BookManager(IBookDal bookDal, IWriterService writerService , ICategoryService categoryService, IPublisherService publisherService)
		{
			_bookDal = bookDal;
			_writerService = writerService;
			_categoryService = categoryService;
			_publisherService = publisherService;
		}

		public void AddBook(Book book)
		{
			book.IsActive = true;
			_bookDal.Add(book);
		}

		public List<Book> GetAll()
		{
			var books = _bookDal.GetAll().Where(c => c.IsActive).ToList();
			books.ForEach(b =>
			{
				b.Category = _categoryService.GetById(b.CategoryId);
				b.Writer = _writerService.GetById(b.WriterId);
				b.Publisher = _publisherService.GetById(b.PublisherId);
			});
			return books;
		}

		public Book GetById(int id)
		{
			var book = _bookDal.Get(b => b.BookId == id);
			book.Category = _categoryService.GetById(book.CategoryId);
			book.Writer = _writerService.GetById(book.WriterId);
			book.Publisher = _publisherService.GetById(book.PublisherId);
			return book;
		}

		public void RemoveBook(int id)
		{
			var book = GetById(id);
			if (book != null)
			{
				book.IsActive = false;
				_bookDal.Update(book);
			}
		}

		public void UpdateBook(Book book)
		{
			book.IsActive = true;
			_bookDal.Update(book);
		}
	}
}
