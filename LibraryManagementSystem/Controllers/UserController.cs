using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        IMemberService _memberService;
        IBookTransactionService _bookTransactionService;
        
        public UserController(IMemberService memberService, IBookTransactionService bookTransactionService)
        {
            _memberService = memberService;
            _bookTransactionService = bookTransactionService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var member = _memberService.GetAll().Where(m => m.Mail == User.Identity.Name).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public IActionResult Index(Member member)
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

		[HttpGet]
		public IActionResult MyBooks()
		{
			var memberId = _memberService.GetAll().Where(m => m.Mail == User.Identity.Name).FirstOrDefault().MemberId;
			var myBooks = _bookTransactionService.MyBooks(memberId);
			return View(myBooks);
		}

	}
}
