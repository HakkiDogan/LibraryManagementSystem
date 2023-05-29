using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class StaffController : Controller
	{
		IStaffService _staffService;

		public StaffController(IStaffService staffService)
		{
			_staffService = staffService;
		}

		public IActionResult Index()
		{
			var staffs = _staffService.GetAll();
			return View(staffs);
		}

        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStaff(Staff staff)
        {
            _staffService.AddStaff(staff);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveStaff(int id)
        {
            _staffService.RemoveStaff(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var staff = _staffService.GetById(id);
            return View("UpdateStaff", staff);
        }

        [HttpPost]
        public IActionResult UpdateStaff(Staff staff)
        {
            _staffService.UpdateStaff(staff);
            return RedirectToAction("Index");
        }
    }
}
