using CityInfo.DATA;
using CityInfo.DOMAIN.DTOs;
using Microsoft.AspNetCore.Mvc;
using CityInfo.SERVICE.Repository.Interfaces;
using AutoMapper;
using CityInfo.DOMAIN.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private   int PAGE_SIZE = 10;
        public CitiesController(ICityRepository cityRepository, IMapper mapper)
        {
            this._cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTOWithoutPointsOfInterest>>> Read(string? name, 
                                                                                          string? search, 
                                                                                          int PAGE_NUMBER = 1,
                                                                                          int PAGE_SIZE = 10 )
        {
            // to insure  max size of page
            if (PAGE_SIZE > this.PAGE_SIZE)
            { 
                PAGE_SIZE = this.PAGE_SIZE;
            }


            var (Modelresult, paginationMetadata) = await _cityRepository.Read(name,search, PAGE_NUMBER, PAGE_SIZE);

            Response.Headers.Add("x-pagination", JsonSerializer.Serialize(paginationMetadata));

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
