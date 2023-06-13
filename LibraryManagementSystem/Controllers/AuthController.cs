using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        IMemberService _memberService;
		IAdminDal _adminDal;

		public AuthController(IMemberService memberService,IAdminDal adminDal)
		{
			_memberService = memberService;
			_adminDal = adminDal;
		}

        [HttpGet]
		public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
        public async Task<IActionResult> Login(Member member)
        {
			var datavalue = _memberService.GetAll().FirstOrDefault(m => m.Mail == member.Mail && m.Password == member.Password);
            if (datavalue != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,datavalue.Mail)
				};

				var useridentity = new ClaimsIdentity(claims,"login");
				ClaimsPrincipal principal= new ClaimsPrincipal(useridentity);
				await HttpContext.SignInAsync(principal);
                TempData["FullName"] = datavalue.Name.ToString()+" " + datavalue.Surname.ToString();
                return RedirectToAction("Index", "User");
			}
			else
			{
                return View();
            }
			
        }

		[HttpGet]
		public IActionResult AdminLogin()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AdminLogin(Admin admin)
		{
			var datavalue = _adminDal.GetAll().FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);
			if (datavalue != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,datavalue.Email)
				};

				var useridentity = new ClaimsIdentity(claims, "adminlogin");
				ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
				await HttpContext.SignInAsync(principal);
				return RedirectToAction("Index", "Category");
			}
			else
			{
				return View();
			}
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


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
