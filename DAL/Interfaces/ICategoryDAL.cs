using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICategoryDAL : IGenericalDAL<Category>
    {
        public IEnumerable<GetCategories> GetCategories();
    }
}
