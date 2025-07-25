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

        public UserController(EmailSender emailSender)
        {
            this.emailSender = emailSender;
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
                var userFound = userDAL.GetByEmail(user.Email);

                if (userFound.Id != 0)
                    return new JsonResult(PassHelper.VerifyPassword(user.Password, userFound.Password));
                return new JsonResult(false);
            }
            catch (Exception)
            {
                throw;
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
                DateTime createdAt = DateTime.Now;

                var resetToken = new PassResetToken
                {
                    CreatedAt = createdAt,
                    Token = token,
                    UserId = user.Id,
                };

                bool tokenSaved = userDAL.AddToken(resetToken);

                if (tokenSaved)
                {
                    string resetLink = $"https://www.youtube.com/";

                    string html = @$"<!DOCTYPE html>
                                    <html lang=""es"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                        <title>Recuperar contraseña</title>
                                    </head>
                                    <body style=""font-family: 
                                                    Arial, sans-serif; 
                                                    background-color: #f4f4f4; 
                                                    padding: 30px;"">
                                        <table style=""max-width: 600px; 
                                                        margin: auto; 
                                                        background-color: #ffffff; 
                                                        padding: 40px; 
                                                        border-radius: 10px; 
                                                        box-shadow: 0px 0px 10px rgba(0,0,0,0.1);"">
                                            <tr>
                                                <td style=""text-align: center;"">
                                                    <h2 style=""color: #333;"">Recuperación de Contraseña</h2>
                                                    <p style=""font-size: 16px; 
                                                                color: #555;"">
                                                        Recibimos una solicitud para restablecer tu contraseña. 
                                                        Si no hiciste esta solicitud, puedes ignorar este mensaje.
                                                    </p>
                                                    <p style=""font-size: 16px; 
                                                                color: #555;"">
                                                        Para restablecer tu contraseña, haz clic en el botón a continuación:
                                                    </p>
                                                    <a href=""{resetLink}"" style=""display: inline-block; 
                                                                            margin-top: 20px; 
                                                                            padding: 12px 25px; 
                                                                            font-size: 16px; 
                                                                            background-color: #007bff; 
                                                                            color: #fff; 
                                                                            text-decoration: none; 
                                                                            border-radius: 6px;"">
                                                        Restablecer contraseña
                                                    </a>
                                                    <p style=""font-size: 14px; 
                                                                color: #999; 
                                                                margin-top: 30px;"">
                                                        Este enlace expirará en 1 hora por razones de seguridad.
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=""text-align: center; 
                                                            font-size: 12px; 
                                                            color: #aaa; 
                                                            padding-top: 40px;"">
                                                    © 2025 TechStore. Todos los derechos reservados.
                                                </td>
                                            </tr>
                                        </table>
                                    </body>
                                    </html>";


                    await emailSender.SendEmailAsync(
                        user.Email,
                        "Reset your password",
                        html);        

                    return new JsonResult("A verification email was sent to your email address");
                }

                return new JsonResult("There was an error with the password reset process");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
