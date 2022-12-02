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

            if (!await _cityRepository.ReadExists(CityId))
            {
                _logger.LogInformation($"City with id {CityId}, not found when creating point of interest.");
                return NotFound("City NotFound");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not IsValid");
            }

            var model = _mapper.Map<PointsOfInterestCreateDTO, PointsOfInterest>(pointsOfInterestCreate);
            await _cityRepository.CreatePointsOfInterest(CityId, model);

            return CreatedAtRoute("ReadPointOfInterest", new
            {
                CityId = CityId,
                PointOfInterestId = model.Id
            }, pointsOfInterestCreate);


        }


        [HttpPut("{PointOfInterestId}")]
        public async Task<ActionResult> Update(int CityId, int PointOfInterestId, PointsOfInterestCreateDTO pointsOfInterestUpdate)
        { 
            await _cityRepository.UpdatePointsOfInterest(CityId, PointOfInterestId, _mapper.Map<PointsOfInterest>(pointsOfInterestUpdate));

            if (!await _cityRepository.ReadExists(CityId))
            {
                throw new Exception($"City with id {CityId} Not Found When update Points Of Interest");
            }

            var result = await _cityRepository.ReadPointsOfInterestForCity(CityId, PointOfInterestId);

            if (result is null)
            {
                throw new Exception("PointsOfInterests NotFound");
            }

            _mapper.Map(pointsOfInterestUpdate, result);

            await _cityRepository.SaveChangesAsync();


            return NoContent();

        }
    }
}