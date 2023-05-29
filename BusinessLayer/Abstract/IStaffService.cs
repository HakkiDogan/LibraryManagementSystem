using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IStaffService
	{
		List<Staff> GetAll();

		void AddStaff(Staff staff);

		void RemoveStaff(int id);

		void UpdateStaff(Staff staff);

		Staff GetById(int id);
	}
}
