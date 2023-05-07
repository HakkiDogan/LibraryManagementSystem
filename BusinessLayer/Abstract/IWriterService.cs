using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IWriterService
	{
		List<Writer> GetAll();

		void AddWriter(Writer writer);

		void RemoveWriter(int id);

		void UpdateWriter(Writer writer);

		Writer GetById(int id);
	}
}
