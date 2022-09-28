using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyClothes.Data.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; } =  Guid.NewGuid().ToString();

        [StringLength(15)]
        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

    }
}
