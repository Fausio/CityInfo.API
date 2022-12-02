using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using Microsoft.AspNetCore.Mvc;
using CityInfo.SERVICE.Repository.Interfaces;
using AutoMapper;
using CityInfo.DOMAIN.Models;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICityRepository cityRepository, IMapper mapper)
        {
            this._cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTOWithoutPointsOfInterest>>> Read([FromQuery(Name ="name")]string? name)
        {
            var Modelresult = await _cityRepository.Read(name);
            var DTOresult = _mapper.Map<IEnumerable<City>, IEnumerable<CityDTOWithoutPointsOfInterest>>(Modelresult);

            return Ok(DTOresult);
        }

     //   [HttpGet("{Id},{includePointsOfInterest}")]
       [HttpGet("{Id}")]
        public async Task<IActionResult> Read(int Id, bool includePointsOfInterest = false)
        {
            var result = await _cityRepository.Read(Id, includePointsOfInterest);
           
            if (result is null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDTO>(result));
            }
            else
            {
                return Ok(_mapper.Map<CityDTOWithoutPointsOfInterest>(result));
            } 
        }
    }
}
