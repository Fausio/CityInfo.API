

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
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite("Data Source=CityInfo.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PointsOfInterest>();
            modelBuilder.Entity<City>().HasData(

                new City("Maputo")
                {
                    Id = 1,
                    Description = "Cidade das acacias",
                    PointsOfInterests = new List<PointsOfInterest>()
                   {
                        new PointsOfInterest("Shoping Center")
                        {
                           
                            CityId = 1,
                            Description ="Maior Shopping do pais até 2010"
                        }
                   }
                },
                new City("Matola")
                {
                    Id = 2,
                    Description = "Privincia de Maputo",
                    PointsOfInterests = new List<PointsOfInterest>()
                                {
                                     new PointsOfInterest("Cinema Lusumundo")
                                     {
                                         Id=1,
                                         CityId=2,
                                         Description ="Jardim da Matola"
                                     }, new PointsOfInterest("Santorine")
                                     {
                                         Id=2,
                                           CityId=2,
                                         Description ="Restourante e Bar"
                                     },
                                }

                },
                new City("Boane")
                {
                    Id = 3,
                    Description = "Cidade das Cabras 🐐",
                    PointsOfInterests = new List<PointsOfInterest>()
                                {
                                     new PointsOfInterest("Banana Landia")
                                     {
                                         Id=3,
                                           CityId=3,
                                         Description ="Pomar de Banana"
                                     }
                                }
                }


                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
