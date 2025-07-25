using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserDAL : IGenericalDAL<User>
    {
        public User GetByEmail(string email);
        public bool AddToken(PassResetToken entity);
    }
}
