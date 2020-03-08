using Microsoft.EntityFrameworkCore;

namespace Regionalizer.Entities
{
    public class RegionalizerDbContext : DbContext
    {
        public RegionalizerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegionMunicipality>().HasKey(rm => new { rm.RegionId, rm.MunicipalityId });
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<RegionMunicipality> RegionMunicipalities { get; set; }
    }
}