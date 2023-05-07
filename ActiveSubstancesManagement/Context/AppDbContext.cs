﻿using Microsoft.EntityFrameworkCore;

namespace ActiveSubstancesManagement.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }


}