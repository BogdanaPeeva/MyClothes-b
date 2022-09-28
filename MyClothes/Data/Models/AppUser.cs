
namespace MyClothes.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    public class AppUser : IdentityUser//, IAuditInfo, IDeletableEntity
    {
        public AppUser()
        {

            this.Id = Guid.NewGuid().ToString();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Garments = new HashSet<UserGarment>();
            this.UserClothesCollection = new HashSet<Garment>();
            this.UserCategoriesList = new HashSet<UserCategory>();

        }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public ICollection<UserGarment> Garments { get; set; }
        public ICollection<Garment> UserClothesCollection { get; set; }
        public ICollection<UserCategory> UserCategoriesList { get; set; }

    }
}
