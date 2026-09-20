using Microsoft.AspNetCore.Mvc;

namespace EssamProject.Controllers.VersioningQuerystring.V2
{

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/productQuery")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is version 2.0 of the Product API.");
        }
    }
}
