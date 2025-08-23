using Entities;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static Category Convert(CategoryModel category)
        {
            if (category is null)
                return new Category();

            return new Category()
            {
                Id = category.Id,
                Name = category.CategoryName,
                IsActive = category.IsActive,
            };
        }
        public static CategoryModel Convert(Category category)
        {
            if (category is null)
                return new CategoryModel();

            return new CategoryModel()
            {
                Id = category.Id,
                CategoryName = category.Name,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt
            };
        }
    }

    public class AddCategoryModel
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }

        public static Category Convert(AddCategoryModel category)
        {
            if (category is null)
                return new Category();
            return new Category()
            {
                Name = category.CategoryName,
                IsActive = category.IsActive,
            };
        }
    }

    public class EditCategoryModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public static Category Convert(EditCategoryModel category)
        {
            if (category is null)
                return new Category();
            return new Category()
            {
                Id = category.Id,
                Name = category.CategoryName,
                IsActive = category.IsActive,
            };
        }
    }
}