using Microsoft.AspNetCore.Mvc;

namespace EssamProject.Controllers.VersioningMediaType.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/productMedia")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is version 1.0 of the Product API.");
        }
    }
}
