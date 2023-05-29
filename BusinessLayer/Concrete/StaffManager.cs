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
	public class StaffManager : IStaffService
	{
		IStaffDal _staffDal;

		public StaffManager(IStaffDal staffDal)
		{
			_staffDal = staffDal;
		}

		public void AddStaff(Staff staff)
		{
			staff.IsActive = true;
			_staffDal.Add(staff);
		}

		public List<Staff> GetAll()
		{
			return _staffDal.GetAll().Where(s => s.IsActive).ToList();
		}

		public Staff GetById(int id)
		{
			return _staffDal.Get(s => s.StaffId == id);
		}

		public void RemoveStaff(int id)
		{
			var staff = GetById(id);
			if (staff != null)
			{
				staff.IsActive = false;
				_staffDal.Update(staff);
			}
		}

		public void UpdateStaff(Staff staff)
		{
			staff.IsActive = true;
			_staffDal.Update(staff);
		}
	}
}
