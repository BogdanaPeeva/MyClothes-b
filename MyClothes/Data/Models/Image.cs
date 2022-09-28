namespace MyClothes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;


    public class Image 
    {
        [Key]
        public string ImageId { get; set; } = Guid.NewGuid().ToString();
        public string Extension { get; set; }

        //// The contents of the image is in the file system
       [Required]
        public string RemoteImageUrl { get; set; }

        public string AddedByAppUserId { get; set; }

        public AppUser AddedByAppUser { get; set; }
    }
}
