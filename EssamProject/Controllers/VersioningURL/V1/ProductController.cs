using Microsoft.AspNetCore.Mvc;

namespace EssamProject.Controllers.VersioningURL.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/product")]
    [Route($"api/v{{version:apiVersion}}/product")]
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
