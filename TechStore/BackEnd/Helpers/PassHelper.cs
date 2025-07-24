using BackEnd.Models;

namespace BackEnd.Helpers
{
    public class PassHelper
    {
        public static string HashPassword(string pass)
        { 
            return BCrypt.Net.BCrypt.HashPassword(pass + SecurityModel.SRCP);
        }

        public static bool VerifyPassword(string pass, string hashedPass)
        {
            return BCrypt.Net.BCrypt.Verify(pass + SecurityModel.SRCP, hashedPass);
        }
    }
}
