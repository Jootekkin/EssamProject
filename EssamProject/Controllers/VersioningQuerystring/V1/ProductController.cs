using Microsoft.AspNetCore.Mvc;

namespace EssamProject.Controllers.VersioningQuerystring.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/productQuery")]
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
