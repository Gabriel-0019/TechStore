using Entities;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 25 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]).{8,25}$",
        ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula y un carácter especial.")]
        public string Password { get; set; }

        public User Convert(UserModel user)
        {
            if (user is null)
                return new User();

            return new User()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
            };
        }
    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GetUsers
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public static GetUsers Convert(User user)
        {
            if (user is null) return new GetUsers();

            return new GetUsers()
            {
                Email = user.Email,
                Id = user.Id
            };
        }
    }
}
