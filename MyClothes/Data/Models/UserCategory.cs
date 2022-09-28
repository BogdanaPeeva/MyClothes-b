using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClothes.Data.Models
{
    public class UserCategory
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
