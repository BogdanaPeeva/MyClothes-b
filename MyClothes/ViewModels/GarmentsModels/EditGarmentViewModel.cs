using Microsoft.AspNetCore.Mvc.Rendering;
using MyClothes.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MyClothes.ViewModels.GarmentsModels
{
    public class EditGarmentViewModel
    {
        public string GarmentId { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public SelectList CategoryNames { get; set; }


        [Display(Name = "Season")]
        public string SeasonId { get; set; }
        public string SeasonName { get; set; }
        public SelectList SeasonNames { get; set; }


        [Display(Name = "Colour")]
        public string ColourId { get; set; }
        public string ColourName { get; set; }
        public SelectList ColourNames { get; set; }
    }
}
