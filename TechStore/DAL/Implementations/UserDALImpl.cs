using DAL.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UserDALImpl : IUserDAL
    {
        public bool Add(User entity)
        {
            using var context = new TechStoreDBContext();
            context.Users.Add(entity);
            context.SaveChanges();
            return true;
        }

        public void AddRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            using var context = new TechStoreDBContext();
            IEnumerable<User> users = [.. context.Users];
            return users;
        }

        public User Login(User user)
        {
            using var context = new TechStoreDBContext();
            var userFound = context.Users.Where(u => u.Email == user.Email);
            if (userFound.Any())
                return userFound.First();
            return new User();
        }

        public bool Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public User SingleOrDefault(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
