using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using X.PagedList;

namespace LibraryManagementSystem.Controllers
{
	public class MemberController : Controller
	{
		IMemberService _memberService;

		public MemberController(IMemberService memberService)
		{
			_memberService = memberService;
		}

		public IActionResult Index(int page = 1)
		{
			var members = _memberService.GetAll().ToPagedList(page, 10);

			return View(members);
		}

        public IActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            MemberValidator memberValidator = new MemberValidator();
            ValidationResult result = memberValidator.Validate(member);
            if (result.IsValid)
            {
                _memberService.AddMember(member);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }

		public IActionResult RemoveMember(int id)
		{
			_memberService.RemoveMember(id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UpdateMember(int id)
		{
			var member = _memberService.GetById(id);
			return View("UpdateMember", member);
		}

		[HttpPost]
		public IActionResult UpdateMember(Member member)
		{
			MemberValidator memberValidator = new MemberValidator();
			ValidationResult result = memberValidator.Validate(member);
			if (result.IsValid)
			{
				_memberService.UpdateMember(member);
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
		}
	}
}
