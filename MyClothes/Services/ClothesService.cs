
namespace MyClothes.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MyClothes.Data;
    using MyClothes.Data.Models;
    using MyClothes.ViewModels.GarmentsModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClothesService : IClothesService
    {

        private readonly ApplicationDbContext dbContext;

        private readonly IMapper mapper;

        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "jfif", "avif" };

        public ClothesService(ApplicationDbContext dbcontext, IMapper mapper)
        {
            this.dbContext = dbcontext;
            this.mapper = mapper;
        }
        
        public async Task<string> AddGarmentAsync(string userId, AppUser user, AddGarmentInputModel addGarmentInputModel, string path)
        {
            var garment = new Garment
            {
                AppUserId = userId,
                AppUser = user
            };

            if (addGarmentInputModel.CategoryId != default)
            {
                garment.CategoryId = addGarmentInputModel.CategoryId;

                garment.Category = await this.dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == addGarmentInputModel.CategoryId);

                garment.Category.Name = this.dbContext.Categories.FirstOrDefault(x => x.CategoryId == addGarmentInputModel.CategoryId).Name;
            }
            if (addGarmentInputModel.SeasonId != default)
            {
                garment.SeasonId = addGarmentInputModel.SeasonId;

                garment.Season = await this.dbContext.Seasons.FirstOrDefaultAsync(x => x.SeasonId == addGarmentInputModel.SeasonId);

                garment.Season.Name = this.dbContext.Seasons.FirstOrDefault(x => x.SeasonId == addGarmentInputModel.SeasonId).Name;
            }

            if (addGarmentInputModel.ColourId != default)
            {

                garment.ColourId = addGarmentInputModel.ColourId;

                garment.Colour = await dbContext.Colours.FirstOrDefaultAsync(x => x.ColourId == addGarmentInputModel.ColourId);

                garment.Colour.Name = dbContext.Colours.FirstOrDefault(x => x.ColourId == addGarmentInputModel.ColourId).Name;

            }

            // /wwwroot/images/clothes/jhdsi-343g3h453-=g34g.jpg

            if (addGarmentInputModel.Images != default)
            {

                Directory.CreateDirectory($"{path}/clothes/");

                var extension = Path.GetExtension(addGarmentInputModel.Images.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {

                    AddedByAppUserId = userId,
                    Extension = extension,

                };

                if (dbImage.RemoteImageUrl == null)
                {
                    dbImage.RemoteImageUrl = "/images/clothes/" + dbImage.ImageId + "." + dbImage.Extension;
                }

                garment.ImageId = dbImage.ImageId;

                var physicalPath = $"{path}/clothes/{dbImage.ImageId}.{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

                await addGarmentInputModel.Images.CopyToAsync(fileStream);

                garment.ImageUrl = dbImage.RemoteImageUrl;

                await this.dbContext.Images.AddAsync(dbImage);

            }
            var garmentModel = new GarmentViewModel();

            garmentModel = this.mapper.Map<GarmentViewModel>(garment);

            if (await this.dbContext.Garments.FirstOrDefaultAsync(x => x.GarmentId == garment.GarmentId) != null)
            {
                throw new Exception("Picture already exist!");
            }

            var userGarment = new UserGarment()
            {
                AppUserId = userId,
                GarmentId = garment.GarmentId
            };

            await this.dbContext.Garments.AddAsync(garment);
            // todo:
            dbContext.AppUsers.FirstOrDefault(x => x.Id == userId).UserClothesCollection
                .Add(garment);

            dbContext.AppUsers.FirstOrDefault(x => x.Id == userId).Garments.Add(userGarment);

            await dbContext.UserGarments.AddAsync(userGarment);

            await dbContext.SaveChangesAsync();

            return garmentModel.GarmentId;
        }

        //TODO: AddToCollection
        public async Task AddPictureToUserGarments(string userId, string garmentId)
        {
            if (await this.dbContext.UserGarments.FirstOrDefaultAsync(x => x.AppUserId == userId && x.GarmentId == garmentId) != null)
            {
                throw new Exception("Already exist!");
            }

            await this.dbContext.UserGarments.AddAsync(new UserGarment
            {
                GarmentId = garmentId,
                AppUserId = userId,
            });

            await this.dbContext.SaveChangesAsync();
        }
        public async Task<EditGarmentViewModel> GetEditGarmentModel(string id)
        {

            Garment garment = await this.dbContext.Garments.FindAsync(id);

            if (garment == null)
            {
                throw new Exception("Garment doesn't exist!");
            }

            EditGarmentViewModel editGarmentModel = this.mapper.Map<EditGarmentViewModel>(garment);

            return editGarmentModel;
        }
        public async Task<EditGarmentViewModel> EditGarment(string id, EditGarmentViewModel editGarmentInputModel)
        {

            Garment garment =await this.dbContext.Garments.FindAsync(id);

            if (garment == null)
            {
                throw new Exception("Garment doesn't exist!");
            }

            Garment updatedModel = this.mapper.Map(editGarmentInputModel, garment);

            this.dbContext.Update(updatedModel);

            await this.dbContext.SaveChangesAsync();

            EditGarmentViewModel model = this.mapper.Map<EditGarmentViewModel>(updatedModel);
            return model;
        }
        public async Task<GarmentViewModel> DeleteGarmentGet(string id)
        {
            Garment garment = await this.dbContext.Garments.Where(x => x.GarmentId == id).Include(x => x.Category).Include(x => x.Season).Include(x => x.Season).FirstOrDefaultAsync();

            GarmentViewModel garmentViewVModel = this.mapper.Map<GarmentViewModel>(garment);

            return garmentViewVModel;
        }
        public async Task DeleteGarment(string garmentId)
        {

            Garment garment = await this.dbContext.Garments.FindAsync(garmentId);

            var image = await this.dbContext.Images.FindAsync(garment.ImageId);

            this.dbContext.Garments.Remove(garment);

            this.dbContext.Images.Remove(image);

            var pathh = $"wwwroot/{image.RemoteImageUrl }";

            //var path = Path.Combine(Directory.GetCurrentDirectory(), "~", image.RemoteImageUrl);

            if (System.IO.File.Exists(pathh))
            {
                System.IO.File.Delete(pathh);
            }
            await this.dbContext.SaveChangesAsync();
        }
        public async Task<AllPicturesViewModel> GetAll(string userId)
        {

            if (await dbContext.UserGarments.FirstOrDefaultAsync(x => x.AppUserId == userId) == null)
            {
                throw new Exception("No pictures! Please add pictures!");
            }

            List<Garment> garmentList = await this.dbContext.Garments.Where(x => x.AppUserId == userId).Include(x => x.Category).Include(x => x.Season).Include(x => x.Colour).ToListAsync();

            List<GarmentViewModel> garmentModel = this.mapper.Map<List<GarmentViewModel>>(garmentList);


            var allPicturesViewModel = new AllPicturesViewModel()
            {
                GarmentsModelList = garmentModel
            };

            return allPicturesViewModel;
        }


        public async Task<GarmentViewModel> Details(string id)
        {

            Garment garment =await this.dbContext.Garments.Where(x => x.GarmentId == id).Include(x => x.Category).Include(x => x.Season).Include(x => x.Colour).FirstOrDefaultAsync();

            if (garment == null)
            {
                throw new Exception("This garment doesn't exist!");
            }


            GarmentViewModel garmentModel = this.mapper.Map<GarmentViewModel>(garment);

            return garmentModel;
        }

    }
}

