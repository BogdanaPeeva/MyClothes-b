
namespace MyClothes.ViewModels
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyClothes.Data.Models;
    using MyClothes.ViewModels.CategoriesModels;
    using MyClothes.ViewModels.ColoursModels;
    using MyClothes.ViewModels.GarmentsModels;
    using MyClothes.ViewModels.SeasonsModels;
    using System.Collections.Generic;
    using System.Linq;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Clothes Map

            CreateMap<Garment, GarmentViewModel>();
            CreateMap<AddGarmentInputModel, Garment>();
            CreateMap<Garment, AddGarmentInputModel>();
            

            CreateMap<GarmentViewModel, Garment>();
            CreateMap<GarmentViewModel, AddGarmentInputModel>();
            //todo: ne
            CreateMap<AddGarmentInputModel, GarmentViewModel>();
            
            CreateMap<EditGarmentViewModel, Garment>();
            CreateMap<Garment, EditGarmentViewModel>();

            CreateMap<AddGarmentInputModel, SelectList>();
            CreateMap<SelectList, AddGarmentInputModel>();

            CreateMap<AllPicturesViewModel, ICollection<GarmentViewModel>>().ReverseMap();


            // Category Map

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
               



            CreateMap<Category, AddGarmentInputModel>();
            CreateMap<AddGarmentInputModel, Category>();

            CreateMap<Category, CreateCategoryInputModel>();
            CreateMap<CreateCategoryInputModel, Category>();

            CreateMap<EditCategoryModel, Category>();
            CreateMap<Category, EditCategoryModel>();

            // Season Map

            CreateMap<Season, AddGarmentInputModel>();
            CreateMap<AddGarmentInputModel, Season>();

            //Colour Map

            CreateMap<Colour, AddGarmentInputModel>();
            CreateMap<AddGarmentInputModel, Colour>();


            CreateMap<Season, SeasonViewModel>();
            CreateMap<Colour, ColourViewModel>();

        }
    }
}
