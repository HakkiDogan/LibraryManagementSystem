using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        IMemberService _memberService;

		public AuthController(IMemberService memberService)
		{
			_memberService = memberService;
		}

		public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Register(Member member)
		{
			MemberValidator memberValidator = new();
			ValidationResult result = memberValidator.Validate(member);
			if (result.IsValid)
			{
				_memberService.AddMember(member);
				return RedirectToAction("Login");
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
