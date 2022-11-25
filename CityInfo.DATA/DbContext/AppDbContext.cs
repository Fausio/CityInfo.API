

using CityInfo.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DATA.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointsOfInterest> PointsOfInterests { get; set; } = null!;
    }
}
