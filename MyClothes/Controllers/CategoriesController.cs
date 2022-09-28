namespace MyClothes.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyClothes.Data.Models;
    using MyClothes.Services;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;
    using MyClothes.ViewModels.CategoriesModels;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Threading.Tasks;
    using System;
    using MyClothes.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    [AutoValidateAntiforgeryToken]
    public class CategoriesController : Controller
    {
        private readonly IClothesService garmentService;
        private readonly ICategoryService categoryService;
        private readonly ISeasonService seasonService;
        private readonly IColourService colourService;
        //private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public CategoriesController(IClothesService garmentService,
            ICategoryService categoryService,
            ISeasonService seasonService,
            IColourService colourService,
            IMapper mapper,
            IUserService userService,
            UserManager<AppUser> userManager)
        {
            this.garmentService = garmentService;
            this.categoryService = categoryService;
            this.seasonService = seasonService;
            this.colourService = colourService;
            this.userService = userService;
            this.userManager = userManager;
            this.mapper = mapper;
            //this.contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var allCategoriesViewModelList = await this.categoryService.GetAllCategoriesByUserIdAsync(user.Id);

                return this.View(allCategoriesViewModelList);

            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return this.RedirectToAction(nameof(this.All));
            }

        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();

        }
        [HttpPost]
        [Authorize]
        public async Task< IActionResult >Create(CreateCategoryInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var userId =await this.userManager.GetUserIdAsync(user);

               await this.categoryService.CreateCategoryAsync(userId, inputModel);
            }
            catch (Exception ex)
            {
                this.TempData["Message"] =ex.Message;

                return this.RedirectToAction(nameof(this.Create));
            }

            this.TempData["Message"] = "The category added successfully.";

            return this.RedirectToAction(nameof(this.Index));
        }
        [Authorize]
        public async Task<IActionResult> All()
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var allCategoriesViewModelList = await this.categoryService.GetAllCategoriesByUserIdAsync(user.Id);

                return this.View(allCategoriesViewModelList);
            }

            catch (Exception ex)
            {
                this.TempData["Message"] =ex.Message;


                return this.RedirectToAction(nameof(this.Create));
            }

        }
        [Authorize]
        public async Task<IActionResult> ByName(string name)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var viewModel =await this.categoryService.GetByName(user.Id, name);

                return this.View(viewModel);
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }

        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var userId = await this.userManager.GetUserAsync(this.User);

                await this.categoryService.DeleteCategory(id, userId.Id);

                return RedirectToAction(nameof(this.Index));
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (id == "1" || id == "2" || id == "3"
                        || id == "4" || id == "5" || id == "6"
                        || id == "7")
                {
                    this.TempData["Message"] = "Can not edit this category!";

                    return RedirectToAction(nameof(this.Index));
                }

                var editCategoryInputModel = await categoryService.GetEditCategoryModelAsync(id);

                return this.View(editCategoryInputModel);
            }

            catch (Exception exception)
            {

                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditCategoryModel editCategoryViewModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(editCategoryViewModel);
            }
            try
            {
                var model = await this.categoryService.EditCategoryAsync(id, editCategoryViewModel);

                return RedirectToAction(nameof(this.All), new { id = model.CategoryId });
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.Index));
            }
        }
    }
}
