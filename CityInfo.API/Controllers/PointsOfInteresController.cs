using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using CityInfo.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/Cities/{CityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInteresController : ControllerBase
    {
        private readonly ILogger<PointsOfInteresController> _logger;

        public PointsOfInteresController(ILogger<PointsOfInteresController> logger)
        {
            this._logger = logger ?? throw new ArgumentException(null, nameof(logger));
         
            // get direct from container
            //   HttpContext.RequestServices.GetService
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestCreateDTO>> Read(int CityId)
        { 
            try
            {
                throw new Exception($" found");

                CityDTO result = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == CityId);
                if (result is null)
                {
                    throw new Exception(); 
                }

                return Ok(result.PointsOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"City with id {CityId}, wasn't found when accessing points of interest.",ex);
                return StatusCode(500, "An error occurred at the server level");
            }
            
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
        public ActionResult Create(int CityId, [FromBody] PointsOfInterestCreateDTO pointsOfInterestCreate)
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


        [HttpPut("{PointOfInterestId}")]
        public ActionResult Update(int CityId, int PointOfInterestId, PointsOfInterestUpdateDTO pointsOfInterestUpdate)
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

            result.Name = pointsOfInterestUpdate.Name;
            result.Description = pointsOfInterestUpdate.Description;

            return NoContent();
        }

        [HttpPatch("{PointOfInterestId}")]
        public ActionResult UpdatePartial(int CityId,
            int PointOfInterestId,
            JsonPatchDocument<PointsOfInterestUpdateDTO> pointsOfInterestUpdate)
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

            var modelPatch = new PointsOfInterestUpdateDTO
            {
                Name = result.Name,
                Description = result.Description
            };

            pointsOfInterestUpdate.ApplyTo(modelPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(modelPatch))
            {
                return BadRequest(ModelState);
            }

            result.Name = modelPatch.Name;
            result.Description = modelPatch.Description;

            return NoContent();
        }

        [HttpDelete("{PointOfInterestId}")]
        public ActionResult Delete(int CityId, int PointOfInterestId)
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

            CityIdresult.PointsOfInterests.Remove(result);
            return NoContent();
        }

    }
}
