using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> Read()
        {
            IEnumerable<CityDTO> result = CitiesDataStore.Instance.Cities;
            return Ok(result);
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
