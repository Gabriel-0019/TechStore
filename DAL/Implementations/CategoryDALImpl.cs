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
    public class CategoryDALImpl : ICategoryDAL
    {
        public bool Add(Category entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
            using var context = new TechStoreDBContext();
            context.Categories.Add(entity);
            var result = context.SaveChanges();
            return result > 0;
        }

        public void AddRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            using var context = new TechStoreDBContext();
            var categoryFound = context.Categories.Where(c => c.Id == id);
            return categoryFound.Any() ? categoryFound.First() : new Category();
        }

        public IEnumerable<Category> GetAll()
        {
            using var context = new TechStoreDBContext();
            return [.. context.Categories.OrderBy(x => x.Name)];
        }

        public bool Remove(Category entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Category SingleOrDefault(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category entity)
        {
            entity.ModifiedAt = DateTime.Now;
            
            using var context = new TechStoreDBContext();
            context.Categories.Attach(entity);
            context.Entry(entity).Property(x => x.ModifiedAt).IsModified = true;
            context.Entry(entity).Property(x => x.Name).IsModified = true;
            context.Entry(entity).Property(x => x.IsActive).IsModified = true;
            
            var result = context.SaveChanges();
            return result > 0;
        }
    }
}
