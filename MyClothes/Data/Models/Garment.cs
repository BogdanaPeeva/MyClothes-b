using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyClothes.Data.Models
{
    public class Garment
    {
        [Key]
        public string GarmentId { get; set; } =  Guid.NewGuid().ToString();
       
        [Required]
        public string ImageUrl { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? SeasonId { get; set; }
        public Season? Season { get; set; }

        public string? ColourId { get; set; }
        public Colour? Colour { get; set; }

        [Required]
        public string ImageId { get; set; }


    }
}
