

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
    public class SeasonService : ISeasonService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public SeasonService(ApplicationDbContext dbContext, IUserService userService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.mapper=mapper;
        }
       
        public SelectList SeasonList()
        {
            return new SelectList(dbContext.Seasons, "SeasonId", "Name");
        }
    }
}
