using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult result= categoryValidator.Validate(category);
            if(result.IsValid)
            {
				_categoryService.AddCategory(category);
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

        public IActionResult RemoveCategory(int id)
        {
            _categoryService.RemoveCategory(id);
            return RedirectToAction("Index");
        }

        
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetById(id);
            return View("GetCategory",category);
        }

        
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        
    }
}
