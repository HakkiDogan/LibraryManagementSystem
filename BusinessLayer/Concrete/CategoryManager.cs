using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void AddCategory(Category category)
        {
            category.IsActive = true;
            _categoryDal.Add(category);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll().Where(c => c.IsActive).ToList();
        }

        public Category GetById(int id)
        {
            return _categoryDal.Get(c=> c.CategoryId == id);
        }

        public void RemoveCategory(int id)
        {
            var category = GetById(id);
            if (category != null)
            {
                category.IsActive = false;
                _categoryDal.Update(category);
            }          
        }

        public void UpdateCategory(Category category)
        {
            category.IsActive = true;
            _categoryDal.Update(category);
        }
    }
}
