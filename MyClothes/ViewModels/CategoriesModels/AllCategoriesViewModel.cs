namespace MyClothes.ViewModels.CategoriesModels
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel
    {
        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}