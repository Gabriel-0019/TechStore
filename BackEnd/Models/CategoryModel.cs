using Entities;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }

        public static Category Convert(CategoryModel category)
        { 
            if (category is null)
                return new Category();

            return new Category()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive,
            };
        }
    }
}
