

namespace MyClothes.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyClothes.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class ColourService : IColourService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public ColourService(ApplicationDbContext dbContext, IMapper mapper, IUserService userService/*,UserManager<AppUser> userManager*/)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.mapper=mapper;    
        }
        public void AddColour()
        {

        }
        public SelectList ColourList()
        {
            return new SelectList(dbContext.Colours, "ColourId", "Name");
        }
       
    }
}
