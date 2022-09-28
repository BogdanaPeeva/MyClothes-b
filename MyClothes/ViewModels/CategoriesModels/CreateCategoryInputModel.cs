using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyClothes.ViewModels.CategoriesModels
{
    public class CreateCategoryInputModel
    {
        //public string CategoryId { get; set; }
        [MinLength(3)]
        [MaxLength(12)]
        [Required]
        [Display(Name = "Category Name")]
        public string  CategoryName { get; set; }
        public int CategoryClothesCollectionCount { get; set; }
        public string Url => $"/c/{this.CategoryName.Replace(" ", "-").Trim('-')}";

    }
}
