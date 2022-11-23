using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/Cities/{CityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInteresController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestDTO>> Read(int CityId)
        {
            CityDTO result = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == CityId);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.PointsOfInterests);
        }

        [HttpGet("{PointOfInterestId}")]
        public ActionResult<PointsOfInterestDTO> Read(int CityId,int PointOfInterestId)
        {
            CityDTO CityIdresult = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == CityId);
            if (CityIdresult is null)
            {
                return NotFound("City NotFound");
            }

            var result = CityIdresult.PointsOfInterests.FirstOrDefault(x => x.Id == PointOfInterestId);
            if (result is null)
            {
                return NotFound("PointsOfInterests NotFound");
            }

            return Ok(result);
        }
    }
}
