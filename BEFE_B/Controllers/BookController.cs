using Microsoft.AspNetCore.Mvc;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok();
        }
    }
}
