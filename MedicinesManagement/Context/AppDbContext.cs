using MedicinesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicinesManagement.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<ActiveSubstance> ActiveSubstances { get; set; }
        public virtual DbSet<MedicineActiveSubstance> MedicineActiveSubstances { get; set; }
        public virtual DbSet<ATCCategory> ATCCategories { get; set; }
        public virtual DbSet<MedicineATCCategory> MedicineATCCategories { get;set; }
    }
}
