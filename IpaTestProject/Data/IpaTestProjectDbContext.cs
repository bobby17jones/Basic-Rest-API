using IpaTestProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IpaTestProject.Data
{
    public class IpaTestProjectDbContext : DbContext
    {
        public IpaTestProjectDbContext(DbContextOptions<IpaTestProjectDbContext> options): base(options)
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
