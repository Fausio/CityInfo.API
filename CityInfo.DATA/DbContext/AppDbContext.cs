

using CityInfo.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DATA.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointsOfInterest> PointsOfInterests { get; set; } = null!;
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CityInfo.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
