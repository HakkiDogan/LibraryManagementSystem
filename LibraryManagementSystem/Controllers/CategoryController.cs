using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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
            _categoryService.AddCategory(category);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCategory(int id)
        {
            _categoryService.RemoveCategory(id);
            return RedirectToAction("Index");
        }
        
    }
}
