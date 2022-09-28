
namespace MyClothes.ViewModels.GarmentsModels
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyClothes.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class AddGarmentInputModel
    {

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please select a file!(.jpg, .png, .avi)")]
        public IFormFile Images { get; set; }

        //public IEnumerable<IFormFile> Images { get; set; }

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
