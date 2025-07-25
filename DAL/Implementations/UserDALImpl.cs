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
            var result = context.SaveChanges();
            return result > 0;
        }

        public void AddRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public bool AddToken(PassResetToken entity)
        {
            using var context = new TechStoreDBContext();
            context.PassResetTokens.Add(entity);
            var result = context.SaveChanges();
            return result > 0;
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            using var context = new TechStoreDBContext();
            var userFound = context.Users.Where(u => u.Id == id);
            return userFound.Any() ? userFound.First() : new User();
        }

        public IEnumerable<User> GetAll()
        {
            using var context = new TechStoreDBContext();
            IEnumerable<User> users = [.. context.Users];
            return users;
        }

        public User GetByEmail(string email)
        {
            using var context = new TechStoreDBContext();
            var userFound = context.Users.Where(u => u.Email == email);
            if (userFound.Any())
                return userFound.First();
            return new User();
        }

        public PassResetToken GetTokenPass(string token)
        {
            using var context = new TechStoreDBContext();
            var tokenFound = context.PassResetTokens.Where(u => u.Token == token);
            return tokenFound.Any() ? tokenFound.First() : new PassResetToken();
        }

        public bool Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public bool DeleteToken(PassResetToken passResetToken)
        {
            using var context = new TechStoreDBContext();
            context.PassResetTokens.Remove(passResetToken);
            var result = context.SaveChanges();
            return result > 0;
        }

        public User SingleOrDefault(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(User entity)
        {
            entity.Password = entity.Password;
            using var context = new TechStoreDBContext();
            context.Users.Attach(entity);
            context.Entry(entity).Property(x => x.Password).IsModified = true;

            int result = context.SaveChanges();
            return result > 0;
        }
    }
}
