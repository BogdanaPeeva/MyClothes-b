namespace MyClothes.Services
{
    using MyClothes.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserService
    {
        string GetCurrentUserId();
        Task<AppUser> GetCurrentUserAsync();

    }
}
