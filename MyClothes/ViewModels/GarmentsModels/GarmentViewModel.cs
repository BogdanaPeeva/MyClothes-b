using MyClothes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClothes.ViewModels.GarmentsModels
{
    public class GarmentViewModel
    {
        public string GarmentId { get; set; }
        public string ImageUrl { get; set; }
        public string? CategoryName { get; set; } = "N/A";
        public string? SeasonName { get; set; } = "N/A";
        public string? ColourName { get; set; } = "N/A";

        public string Description { get => $"Category: {this.CategoryName  ?? "not aded"} <br> Season: {this.SeasonName ?? "not aded "} <br> Colour: {this.ColourName ?? "not aded"}"; }



    }
}
