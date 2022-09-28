
namespace MyClothes.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyClothes.Data.Models;
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> appUser)
        {
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // todo:
            //appUser
            //    .HasMany(e => e.Roles)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            appUser
            .HasMany(au => au.Garments)
            .WithOne()
            .HasForeignKey(ur => ur.AppUserId);

            appUser
            .HasMany(au => au.UserClothesCollection)
            .WithOne(u => u.AppUser)
           .OnDelete(DeleteBehavior.Restrict);


        }


    }
}
