using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using CityInfo.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/Cities/{CityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInteresController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestCreateDTO>> Read(int CityId)
        {
            CityDTO result = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == CityId);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.PointsOfInterests);
        }

        [HttpGet("{PointOfInterestId}", Name = "ReadPointOfInterest")]
        public ActionResult<PointsOfInterest> Read(int CityId, int PointOfInterestId)
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

        [HttpPost]
        public ActionResult  Create(int CityId, [FromBody] PointsOfInterestCreateDTO pointsOfInterestCreate)
        {
            CityDTO CityIdresult = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == CityId);
            if (CityIdresult is null)
            {
                return NotFound("City NotFound");
            }

          
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not IsValid");
            }

            var NewpointsOfInterestCreate = new PointsOfInterest()
            {
                Id = CitiesDataStore.Instance.Cities.SelectMany(points => points.PointsOfInterests).Max(p => p.Id) + 1,
                Name = pointsOfInterestCreate.Name,
                Description = pointsOfInterestCreate.Description
            };

            CityIdresult.PointsOfInterests.Add(NewpointsOfInterestCreate);

            return CreatedAtRoute("ReadPointOfInterest", new
            {
                CityId = CityIdresult.Id,
                PointOfInterestId = NewpointsOfInterestCreate.Id
            }, NewpointsOfInterestCreate);
        }
    }
}
