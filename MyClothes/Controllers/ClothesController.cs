
namespace MyClothes.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyClothes.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Threading.Tasks;
    using MyClothes.Services;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;
    using System.Linq;
    using MyClothes.ViewModels.GarmentsModels;
    // todo: push
    [AutoValidateAntiforgeryToken]
    public class ClothesController : Controller
    {
        private readonly IClothesService garmentService;
        private 
            ICategoryService categoryService;
        private readonly ISeasonService seasonService;
        private readonly IColourService colourService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        private readonly string wwwrootDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images");

        public ClothesController(IClothesService garmentService,
            ICategoryService categoryService,
            ISeasonService seasonService,
            IColourService colourService,
            IMapper mapper,
             IWebHostEnvironment environment,
        UserManager<AppUser> userManager)
        {
            this.garmentService = garmentService;
            this.categoryService = categoryService;
            this.seasonService = seasonService;
            this.colourService = colourService;
            this.environment = environment;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }
        [Authorize]
        public async Task<IActionResult> Add()
        {

            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);

            var categoriesNames = await categoryService.CategoryList(userId);
            var seasonNames = seasonService.SeasonList();
            var colourNames = colourService.ColourList();
            var addGarmentInputModel = new AddGarmentInputModel()
            {
                CategoryNames = categoriesNames,
                SeasonNames = seasonNames,
                ColourNames = colourNames,
            };

            return this.View(addGarmentInputModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddGarmentInputModel addGarmentInputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);

            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(addGarmentInputModel);
                }
              
                var path = $"{this.environment.WebRootPath}/images";

                var garmentId = await this.garmentService.AddGarmentAsync(userId, user, addGarmentInputModel, path);

                this.TempData["Message"] = "Picture added successfully.";

                return RedirectToAction(nameof(this.All), new { id = garmentId });
            }
            catch (Exception exception)
            {
                var categoriesNames = await categoryService.CategoryList(userId);
                var seasonNames = seasonService.SeasonList();
                var colourNames = colourService.ColourList();
                var addGarmentInputModelError = new AddGarmentInputModel()
                {
                    CategoryNames = categoriesNames,
                    SeasonNames = seasonNames,
                    ColourNames = colourNames,
                };

                this.TempData["Message"] = exception.Message;

                return this.View(addGarmentInputModelError);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                GarmentViewModel garmentModel = await this.garmentService.Details(id);
                return View(garmentModel);
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.All));
            }
        }
        [Authorize]
        public async Task<IActionResult> All()
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var userId = await this.userManager.GetUserIdAsync(user);
              
                var allPicturesViewModel =await garmentService.GetAll(userId);

                return this.View(allPicturesViewModel);
            }
            catch (Exception exception)
            {

                this.TempData["Message"] = exception.Message;

                return this.RedirectToAction(nameof(this.Index));

            }
        }

        [HttpPost]
        [Authorize]
        // todo: check
        public async Task<IActionResult> AddToCollection(string garmentId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user =await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);

            await this.garmentService.AddPictureToUserGarments(userId, garmentId);

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);


            try
            {
                var categoriesNames = await categoryService.CategoryList(userId);
                var seasonNames = seasonService.SeasonList();
                var colourNames = colourService.ColourList();
                var editGarmentInputModel = new EditGarmentViewModel()
                {
                    GarmentId = id,
                    CategoryNames = categoriesNames,
                    SeasonNames = seasonNames,
                    ColourNames = colourNames,
                };

                return this.View(editGarmentInputModel);
            }
            catch (Exception exception)
            {
                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.All));

            };
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditGarmentViewModel editGarmentViewModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(editGarmentViewModel);
            }
            try
            {
                var model = await this.garmentService.EditGarment(id, editGarmentViewModel);

                this.TempData["Message"] = "Picture was edit successfully.";

                return RedirectToAction(nameof(this.Details), new { id = model.GarmentId });
            }
            catch (Exception exception)
            {

                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.All));
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var garmentViewModel =await this.garmentService.DeleteGarmentGet(id);

            return this.View(garmentViewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeletePost(string garmentId)
        {

            try
            {
                await this.garmentService.DeleteGarment(garmentId);

                return RedirectToAction(nameof(this.All));
            }
            catch (Exception exception)
            {

                this.TempData["Message"] = exception.Message;

                return RedirectToAction(nameof(this.All));
            }
        }

    }
}

