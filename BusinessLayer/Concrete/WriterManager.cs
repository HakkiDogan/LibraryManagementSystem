using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class WriterManager : IWriterService
	{
		IWriterDal _writerDal;

		public WriterManager(IWriterDal writerDal)
		{
			_writerDal = writerDal;
		}

		public void AddWriter(Writer writer)
		{
			writer.IsActive = true;
			_writerDal.Add(writer);
		}

		public List<Writer> GetAll()
		{
			return _writerDal.GetAll().Where(w => w.IsActive).ToList();
		}

		public Writer GetById(int id)
		{
			return _writerDal.Get(c => c.WriterId == id);
		}

		public void RemoveWriter(int id)
		{
			var writer = GetById(id);
			if (writer != null)
			{
				writer.IsActive = false;
				_writerDal.Update(writer);
			}
		}

		public void UpdateWriter(Writer writer)
		{
			writer.IsActive = true;
			_writerDal.Update(writer);
		}
	}
}
