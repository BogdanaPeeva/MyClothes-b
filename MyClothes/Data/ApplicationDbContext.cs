namespace MyClothes.Data
{
    using Microsoft.AspNetCore.Identity;
    using MyClothes.Data.Models;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Garment> Garments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserGarment> UserGarments { get; set; }
        public DbSet<UserCategory> UsersCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserGarment>(usergarment =>
            {
                usergarment.HasKey(ug => new { ug.AppUserId, ug.GarmentId });

            });
            builder.Entity<UserCategory>(userCategory =>
            {
                userCategory.HasKey(uc => new { uc.AppUserId, uc.CategoryId });

            });

            //TODO Check
            //builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });

            //TODO Check

            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            builder.Seed();

        }

        private void ConfigureUserIdentityRelations(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}

