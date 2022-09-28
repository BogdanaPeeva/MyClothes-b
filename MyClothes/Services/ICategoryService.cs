namespace MyClothes.Services
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyClothes.Data.Models;
    using MyClothes.ViewModels.CategoriesModels;
    using MyClothes.ViewModels.GarmentsModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface ICategoryService
    {
       Task< SelectList> CategoryList(string userId);
        Task CreateCategoryAsync(string userId, CreateCategoryInputModel inputModel);
        Task<AllCategoriesViewModel> GetAllCategoriesByUserIdAsync(string userId);
        Task<AllPicturesViewModel>GetByName(string userId, string name);
        Task DeleteCategory(string id, string userId);
        Task<EditCategoryModel> EditCategoryAsync(string id, EditCategoryModel editCategoryInputModel);
        Task<EditCategoryModel> GetEditCategoryModelAsync(string id);
       
    }
}
