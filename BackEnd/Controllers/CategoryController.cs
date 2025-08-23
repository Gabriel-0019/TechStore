using BackEnd.Models;
using DAL.Implementations;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryDAL categoryDAL = new CategoryDALImpl();

        [HttpPost]
        public JsonResult Add([FromBody] AddCategoryModel category)
        {
            try
            {
                return new JsonResult(categoryDAL.Add(AddCategoryModel.Convert(category)));
            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var categories = categoryDAL.GetAll();
                List<CategoryModel> listCategories = [];
                foreach (var item in categories)
                {
                    listCategories.Add(CategoryModel.Convert(item));
                }
                return new JsonResult(listCategories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public JsonResult Edit([FromBody] EditCategoryModel category)
        {
            try
            {
                var existingCategory = categoryDAL.Get(category.Id);
                if (existingCategory == null || existingCategory.Id == 0)
                {
                    return new JsonResult(false);
                }
                existingCategory.Name = category.CategoryName;
                existingCategory.IsActive = category.IsActive;
                return new JsonResult(categoryDAL.Update(existingCategory));
            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }
    }
}
