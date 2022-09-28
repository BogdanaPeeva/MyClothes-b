
namespace MyClothes.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyClothes.Data;
    using MyClothes.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyClothes.ViewModels.CategoriesModels;
    using MyClothes.ViewModels.GarmentsModels;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateCategoryAsync(string userId, CreateCategoryInputModel inputModel)
        {
            var category = new Category
            {
                Name = inputModel.CategoryName.Trim(' '),
            };

            if (await this.dbContext.UsersCategories.FirstOrDefaultAsync(x => x.AppUserId == userId && x.CategoryId == category.CategoryId) != null)
            {
                throw new Exception("This category already exist!");
            }

            var userCategory = new UserCategory()
            {
                AppUserId = userId,
                CategoryId = category.CategoryId,
            };

            await this.dbContext.UsersCategories.AddAsync(userCategory);

            var categories = await dbContext.Categories.ToListAsync();

            var editCategories = new List<Category>();

            editCategories.AddRange(categories);

            foreach (var categoryIn in editCategories)
            {
                var categoryId = categoryIn.CategoryId;

                if (categoryId == "1" || categoryId == "2" || categoryId == "3" || categoryId == "4" || categoryId == "5" || categoryId == "6" || categoryId == "7")
                {
                    if (await this.dbContext.UsersCategories.FirstOrDefaultAsync(x => x.AppUserId == userId && x.CategoryId == categoryId) == null)
                    {
                        await dbContext.UsersCategories.AddAsync(new UserCategory
                        {
                            CategoryId = categoryIn.CategoryId,
                            AppUserId = userId,
                        });
                    }
                }

            }

            if (await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == category.Name) == null)
            {
                await dbContext.Categories.AddAsync(category);
            }

            await dbContext.SaveChangesAsync();

        }
        public async Task<AllCategoriesViewModel> GetAllCategoriesByUserIdAsync(string userId)
        {

            var categories = await dbContext.Categories.ToListAsync();

            var userCategories = await this.dbContext.UsersCategories.Where(x => x.AppUserId == userId).ToListAsync();

            if (userCategories.Count == 0)
            {
                foreach (var categoryIn in categories)
                {
                    var categoryId = categoryIn.CategoryId;

                    if (categoryId == "1" || categoryId == "2" || categoryId == "3"
                        || categoryId == "4" || categoryId == "5" || categoryId == "6"
                        || categoryId == "7")
                    {

                        if (await this.dbContext.UsersCategories.FirstOrDefaultAsync(x => x.AppUserId == userId && x.CategoryId == categoryId) == null)
                        {
                            await dbContext.UsersCategories.AddAsync(new UserCategory
                            {
                                CategoryId = categoryIn.CategoryId,
                                AppUserId = userId,
                            });
                        }
                        else
                        {
                            throw new Exception("This category already exist!");
                        }
                    }
                }
            }

            await dbContext.SaveChangesAsync();

            var createCategoryViewModelList = await this.dbContext.UsersCategories.Where(x => x.AppUserId == userId)
                .Include(x => x.Category)
                .Select(i => new CategoryViewModel()
                {
                    CategoryId = i.CategoryId,

                    CategoryName = i.Category.Name,

                    ImageUrl = i.Category.ImageUrl,

                    CategoryClothesCollectionCount = dbContext.Garments
                .Where(x => x.AppUserId == userId && x.CategoryId == i.CategoryId).Count(),
                })
               .OrderBy(x => x.CategoryName)
               .ToListAsync();

            var allCategoriesVM = new AllCategoriesViewModel
            {
                Categories = createCategoryViewModelList
            };

            return allCategoriesVM;
        }
        public async Task<SelectList> CategoryList(string userId)
        {
            var addCategoriesToDropDownMenu = await this.GetAllCategoriesByUserIdAsync(userId);

            var categories = await dbContext.UsersCategories
                .Where(x => x.AppUserId == userId)
                .Select(x => x.Category).OrderBy(x => x.Name).ToListAsync();

            return new SelectList(categories, "CategoryId", "Name");
        }

        public async Task<AllPicturesViewModel> GetByName(string userId, string name)
        {
            Category category = await this.dbContext.Categories.FirstOrDefaultAsync(x => x.Name == name);

            string categoryId = category.CategoryId;

            if (category == null)
            {
                throw new Exception("No pictures in this category!");
            }

            var garmentList = new List<Garment>();

            garmentList = await this.dbContext.Garments.Where(x => x.CategoryId == categoryId && x.AppUserId == userId).ToListAsync();

            List<GarmentViewModel> garmentModel = this.mapper.Map<List<GarmentViewModel>>(garmentList);

            var allPicturesViewModel = new AllPicturesViewModel()
            {
                GarmentsModelList = garmentModel
            };

            return allPicturesViewModel;
        }

        public async Task DeleteCategory(string id, string userId)
        {
            Category category = await this.dbContext.Categories.FindAsync(id);

            if (id != "1" && id != "2" && id != "3"
                        && id != "4" && id != "5" && id != "6"
                        && id != "7")
            {
                dbContext.Categories.Remove(category);

                await this.dbContext.SaveChangesAsync();

                throw new Exception("Category was succesfully deleted!");
            }
            else
            {
                throw new Exception("Can not delete this category!");
            }

        }

        public async Task<EditCategoryModel> GetEditCategoryModelAsync(string id)
        {

            Category category = await this.dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("The category doesn't exist!");
            }

            EditCategoryModel editCategoryModel = this.mapper.Map<EditCategoryModel>(category);

            return editCategoryModel;
        }
        public async Task<EditCategoryModel> EditCategoryAsync(string id, EditCategoryModel editCategoryInputModel)
        {

            Category category = await this.dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("The category doesn't exist!");
            }

            EditCategoryModel editCategoryViewModelTrimed = editCategoryInputModel;

            editCategoryViewModelTrimed.Name = editCategoryInputModel.Name.Trim(' ');

            Category updatedModel = this.mapper.Map(editCategoryViewModelTrimed, category);

            this.dbContext.Update(updatedModel);

            await this.dbContext.SaveChangesAsync();

            EditCategoryModel model = this.mapper.Map<EditCategoryModel>(updatedModel);

            return model;
        }
    }
}
