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
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetCategories> GetCategories()
        {
            using var context = new TechStoreDBContext();
            var categories = (from l1 in context.Categories
                              where l1.ParentCategoryId == null
                              from l2 in context.Categories
                                  .Where(c => c.ParentCategoryId == l1.Id)
                                  .DefaultIfEmpty()
                              from l3 in context.Categories
                                  .Where(c => l2 != null && c.ParentCategoryId == l2.Id)
                                  .DefaultIfEmpty()
                              select new GetCategories
                              {
                                  CategoryId = l1.Id,
                                  Category = l1.Name,
                                  SubCategory1Id = l2 != null ? l2.Id : (int?)null,
                                  SubCategory1 = l2 != null ? l2.Name : null,
                                  SubCategory2Id = l3 != null ? l3.Id : (int?)null,
                                  SubCategory2 = l3 != null ? l3.Name : null
                              }).ToList();

            return categories;
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
            throw new NotImplementedException();
        }
    }
}
