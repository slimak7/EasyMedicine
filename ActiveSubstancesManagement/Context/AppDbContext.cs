using ActiveSubstancesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ActiveSubstancesManagement.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Interaction> Interactions { get; set; }
        public virtual DbSet<InteractionLevel> InteractionsLevels { get; set; }
    }


}
