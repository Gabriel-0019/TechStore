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
        public JsonResult Add([FromBody] CategoryModel category)
        {
            try
            {
                return new JsonResult(categoryDAL.Add(CategoryModel.Convert(category)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                return new JsonResult(categoryDAL.GetCategories());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
