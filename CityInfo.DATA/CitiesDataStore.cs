using CityInfo.DOMAIN.DTOs;

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
                    Description ="Cidade das acacias"
                },
                new CityDTO()
                            {
                                Id = 2,
                                Name ="Matola",
                                Description ="Privincia de Maputo"
                            },
                new CityDTO()
                            {
                                Id = 3,
                                Name ="Boane",
                                Description ="Cidade das Cabras 🐐"
                            }
            };
        }
    }
}