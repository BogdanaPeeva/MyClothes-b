
namespace MyClothes.ViewModels.CategoriesModels
{
    public class CategoryViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryClothesCollectionCount { get; set; }
        public string? ImageUrl { get; set; }
        public string Url => $"/c/{this.CategoryName.Replace(" ", "-").Trim('-')}";
    }
}
