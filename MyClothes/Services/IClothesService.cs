
namespace MyClothes.Services
{
    using MyClothes.Data.Models;
    using MyClothes.ViewModels.CategoriesModels;
    using MyClothes.ViewModels.GarmentsModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClothesService
    {
        Task<string> AddGarmentAsync(string userId, AppUser user, AddGarmentInputModel addGarmentInputModel, string imagePath);

        Task AddPictureToUserGarments(string userId, string garmentId);

        Task<EditGarmentViewModel> EditGarment(string id, EditGarmentViewModel editGarmentInputModel);


        Task<AllPicturesViewModel> GetAll(string userId);

        Task<EditGarmentViewModel> GetEditGarmentModel(string id);

        Task<GarmentViewModel> Details(string id);
        //Task DeleteGarmentGet(string id);
        Task DeleteGarment(string id);

        Task<GarmentViewModel> DeleteGarmentGet(string id);
    }
}
