namespace MyClothes.Services
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using MyClothes.Data;
    using MyClothes.Data.Models;
    using MyClothes.Services;
    

    public class UsersService : IUserService
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<AppUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public string GetCurrentUserId()
        {
            var currentUserId = this.contextAccessor
                .HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            return currentUserId;
        }

        public async Task<AppUser> GetCurrentUserAsync()
        {
            var currentUser = await this.userManager.GetUserAsync(this.contextAccessor.HttpContext.User);

            return currentUser;
        }
    }
}
