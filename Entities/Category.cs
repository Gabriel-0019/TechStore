using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }

    public class GetCategories
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int? SubCategory1Id { get; set; }
        public string? SubCategory1 { get; set; }
        public int? SubCategory2Id { get;set; }
        public string? SubCategory2 { get; set; }

    }
}
