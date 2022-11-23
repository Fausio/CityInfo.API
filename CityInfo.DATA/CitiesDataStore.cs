using CityInfo.DOMAIN.DTOs;
using CityInfo.DOMAIN.Models;

namespace CityInfo.DATA
{
    public class CitiesDataStore
    {
        public List<CityDTO>? Cities { get; set; }

        public static CitiesDataStore Instance { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name ="Maputo",
                    Description ="Cidade das acacias",
                    PointsOfInterests =    new List<PointsOfInterest>()
                   {
                        new PointsOfInterest()
                        {
                            Id=3,
                            Name ="Shoping Center",
                            Description ="Maior Shopping do pais até 2010"
                        }
                   }
                },
                new CityDTO()
                            {
                                Id = 2,
                                Name ="Matola",
                                Description ="Privincia de Maputo",
                                PointsOfInterests =    new List<PointsOfInterest>()
                                {
                                     new PointsOfInterest()
                                     {
                                         Id=1,
                                         Name ="Cinema Lusumundo",
                                         Description ="Jardim da Matola"
                                     }, new PointsOfInterest()
                                     {
                                         Id=2,
                                         Name ="Santorine",
                                         Description ="Restourante e Bar"
                                     },
                                }

                            },
                new CityDTO()
                            {
                                Id = 3,
                                Name ="Boane",
                                Description ="Cidade das Cabras 🐐",
                                 PointsOfInterests =    new List<PointsOfInterest>()
                                {
                                     new PointsOfInterest()
                                     {
                                         Id=3,
                                         Name ="Banana Landia",
                                         Description ="Pomar de Banana"
                                     }
                                }
                            }
            };
        }
    }
}