using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using Microsoft.AspNetCore.Mvc;
using CityInfo.SERVICE.Repository.Interfaces;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CitiesController(ICityRepository cityRepository)
        {
            this._cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTOWithoutPointsOfInterest>>> Read()
        {
            var result = await _cityRepository.Read();
            var resultTDTO = new List<CityDTOWithoutPointsOfInterest>();

            if (result.Any())
            {
                resultTDTO.AddRange(
                                      result.Select(city => new CityDTOWithoutPointsOfInterest()
                                      {
                                          Id = city.Id,
                                          Name = city.Name,
                                          Description = city.Description
                                      })
                                    );
            }
             
            return Ok(resultTDTO);
        }

        [HttpGet("{Id}")]
        public ActionResult<CityDTO> Read(int Id)
        {

            CityDTO result = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == Id);

            if (result is null)
            {
                return NotFound();
            }


            return Ok(result);
        }
    }
}
