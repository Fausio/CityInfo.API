using AutoMapper;
using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using CityInfo.DOMAIN.Models;
using CityInfo.SERVICE.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/Cities/{CityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInteresController : ControllerBase
    {
        private readonly ILogger<PointsOfInteresController> _logger;
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        public PointsOfInteresController(IMapper mapper,
            ICityRepository cityRepository,
            ILogger<PointsOfInteresController> logger)
        {
            this._cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(ILogger<PointsOfInteresController>));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointsOfInterestDTO>>> Read(int CityId)
        {
            if (!await _cityRepository.ReadExists(CityId))
            {
                _logger.LogInformation($"City with id {CityId}, not found when accessing point of interest.");
                return NotFound();
            }

            var result = await _cityRepository.ReadPointsOfInterestForCity(CityId);
            return Ok(_mapper.Map<IEnumerable<PointsOfInterest>, IEnumerable<PointsOfInterestDTO>>(result));
        }

        [HttpGet("{PointOfInterestId}", Name = "ReadPointOfInterest")]
        public async Task<ActionResult<PointsOfInterestDTO>> Read(int CityId, int PointOfInterestId)
        {
            if (!await _cityRepository.ReadExists(CityId))
            {
                _logger.LogInformation($"City with id {CityId}, not found when accessing point of interest.");
                return NotFound();
            }

            var result = await _cityRepository.ReadPointsOfInterestForCity(CityId, PointOfInterestId);
            if (result is null)
            {
                return NotFound("PointsOfInterests NotFound");
            }

            return Ok(_mapper.Map<PointsOfInterestDTO>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Create(int CityId, [FromBody] PointsOfInterestCreateDTO pointsOfInterestCreate)
        {
            
            if (!await  _cityRepository.ReadExists(CityId))
            {
                _logger.LogInformation($"City with id {CityId}, not found when creating point of interest.");
                return NotFound("City NotFound");
            }
              
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not IsValid");
            }

            //var NewpointsOfInterestCreate = new PointsOfInterestDTO()
            //{
            //    Id = CitiesDataStore.Instance.Cities.SelectMany(points => points.PointsOfInterests).Max(p => p.Id) + 1,
            //    Name = pointsOfInterestCreate.Name,
            //    Description = pointsOfInterestCreate.Description
            //};

            var model = _mapper.Map<PointsOfInterestCreateDTO,PointsOfInterest>(pointsOfInterestCreate);
            await _cityRepository.CreatePointsOfInterest(CityId, model);
          //  await _cityRepository.Read();

            return CreatedAtRoute("ReadPointOfInterest", new
            {
                CityId = CityId,
                PointOfInterestId = model.Id
            }, pointsOfInterestCreate);

            
        }


        //[HttpPut("{PointOfInterestId}")]
        //public ActionResult Update(int CityId, int pointsOfInterestId, PointsOfInterestÙpdateDTO pointsOfInterestUpdate)
        //{
        //    CityDTO CityIdresult = CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == CityId);
        //    if (CityIdresult is null)
        //    {
        //        return NotFound("City NotFound");
        //    }


        //    var result = CityIdresult.PointsOfInterests.FirstOrDefault(x => x.Id == pointsOfInterestId);
        //    if (result is null)
        //    {
        //        return NotFound("PointsOfInterests NotFound");
        //    }

        //    result.Name = pointsOfInterestUpdate.Name;
        //    result.Description = pointsOfInterestUpdate.Description;

        //    return NoContent();

        //}
    }
} 