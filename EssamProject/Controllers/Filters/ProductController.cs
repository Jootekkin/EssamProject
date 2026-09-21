using Microsoft.AspNetCore.Mvc;

namespace EssamProject.Controllers.Filters
{

    [ApiController]
    [Route("api/filters")]
    [CustomFilter] // Apply the custom filter to this controller
    public class FiltersController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is version of the Filters.");
        }
    }
}
