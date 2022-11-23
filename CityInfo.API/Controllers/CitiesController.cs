using CityInfo.DATA;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {

        [HttpGet]
        public JsonResult Read()
        {
            return new JsonResult(CitiesDataStore.Instance.Cities);
        }

        [HttpGet("{Id}")]
        public JsonResult Read(int Id)
        {
            return new JsonResult(CitiesDataStore.Instance.Cities.FirstOrDefault(x => x.Id == Id));
        }
    }
}
