using BackEnd.Helpers;
using BackEnd.Models;
using DAL.Implementations;
using DAL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDAL userDAL;
        private readonly UserModel userModel = new();

        public UserController() 
        {
            userDAL = new UserDALImpl();
        }

        [HttpPost]
        public JsonResult Add([FromBody] UserModel user)
        {
            try
            {
                user.Password = PassHelper.HashPassword(user.Password);
                User entity = userModel.Convert(user);
                return new JsonResult(userDAL.Add(entity));
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
                IEnumerable<User> users = userDAL.GetAll();
                List<GetUsers> listUsers = [];
                foreach (var item in users)
                {
                    listUsers.Add(GetUsers.Convert(item));
                }
                return new JsonResult(listUsers);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("[action]")]
        public JsonResult Login(UserLogin user)
        {
            try
            {
                var userFound = userDAL.Login(UserLogin.Convert(user));

                if (userFound.Id != 0)
                    return new JsonResult(PassHelper.VerifyPassword(user.Password, userFound.Password));
                return new JsonResult(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
