using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        [Required]
        [Display(Name = "Sub Category Name")]
        public string? SubCategoryName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string? SubCategoryDescription { get; set; }

        public int? MainCategoryId { get; set; }
        public bool b_deleted { get; set; }
        [Required]
        [Display(Name = "Main Category")]
        public string? MainCategoryName { get; set; }
        [AllowNull]
        [Display(Name = "Main Categoroy")]
        public int CategoryId { get; set; }
    }
}