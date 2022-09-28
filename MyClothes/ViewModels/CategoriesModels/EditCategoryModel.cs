
namespace MyClothes.ViewModels.CategoriesModels
{
    using System.ComponentModel.DataAnnotations;
    public class EditCategoryModel
    {
        public string CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Shoud be beetween 3 and 20 symbols! ")]
        [MinLength(3)]
        [MaxLength(20),]
        public string Name { get; set; }
        public string? Url => $"/c/{this.Name.Replace(" ", "-").Trim('-')}";

    }
}
