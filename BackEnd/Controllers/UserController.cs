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
        private readonly IUserDAL userDAL = new UserDALImpl();
        private readonly UserModel userModel = new();
        private readonly EmailSender emailSender;
        private readonly EmailTemplate emailTemplate;

        public UserController(EmailSender emailSender, EmailTemplate emailTemplate)
        {
            this.emailSender = emailSender;
            this.emailTemplate = emailTemplate;
        }

        [HttpPost]
        public JsonResult Add([FromBody] UserModel user)
        {
            try
            {
                user.Password = PassHelper.HashPassword(user.Password);
                User entity = userModel.Convert(user);
                return new JsonResult(userDAL.Add(entity)) { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                return new JsonResult("Internal server error: ", ex.ToString()) { StatusCode = 500 };
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
                return new JsonResult(listUsers) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new JsonResult("Internal server error: ", ex.ToString()) { StatusCode = 500 };
            }
        }

        [HttpPost, Route("[action]")]
        public JsonResult Login(UserLogin user)
        {
            try
            {
                var userFound = userDAL.GetByEmail(user.Email);

                if (userFound.Id != 0)
                    return new JsonResult(PassHelper.VerifyPassword(user.Password, userFound.Password)) { StatusCode = 200 };
                return new JsonResult(false) { StatusCode = 401 };
            }
            catch (Exception ex)
            {
                return new JsonResult("Internal server error: ", ex.ToString()) { StatusCode = 500 };
            }
        }

        [HttpPost, Route("[action]")]
        public async Task<JsonResult> RequestPasswordReset(string email)
        {
            try
            {
                var user = userDAL.GetByEmail(email);

                if (user.Id == 0)
                    return new JsonResult("User not found");

                string token = Guid.NewGuid().ToString();

                var resetToken = new PassResetToken
                {
                    CreatedAt = DateTime.Now,
                    Token = token,
                    UserId = user.Id,
                };

                bool tokenSaved = userDAL.AddToken(resetToken);

                if (tokenSaved)
                {
                    string resetLink = $"http://localhost:5173/changepassword/{token}";

                    string html = emailTemplate.GetTemplate("PasswordResetTemplate.html",
                        new Dictionary<string, string>
                        {
                            { "resetLink", resetLink }
                        });

                    await emailSender.SendEmailAsync(
                        user.Email,
                        "Reset your password",
                        html);

                    return new JsonResult("A verification email was sent to your email address") { StatusCode = 200 };
                }

                return new JsonResult("There was an error with the password reset process") { StatusCode = 500 };
            }
            catch (Exception ex)
            {
                return new JsonResult("Internal server error: ", ex.ToString()) { StatusCode = 500 };
            }
        }

        [HttpPost, Route("[action]")]
        public JsonResult ConfirmPassReset(ConfirmPassReset confirmPassReset)
        {
            try
            {
                var resetToken = userDAL.GetTokenPass(confirmPassReset.Token);

                if (resetToken is null || resetToken.CreatedAt.AddHours(1) < DateTime.Now)
                    return new JsonResult("Token expired") { StatusCode = 401 };

                var user = userDAL.Get(resetToken.UserId);

                if (user.Id == 0)
                    return new JsonResult("User not found") { StatusCode = 404 };

                user.Password = PassHelper.HashPassword(confirmPassReset.NewPassword);
                var changed = userDAL.ChangePassword(user);

                if (changed)
                {
                    userDAL.DeleteToken(resetToken);
                    return new JsonResult("Password Changed") { StatusCode = 204 };
                }

                return new JsonResult("System error") { StatusCode = 500 };
            }
            catch (Exception ex)
            {
                return new JsonResult("Internal server error: ", ex.ToString()) { StatusCode = 500 };
            }
        }
    }
}
