using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> HelloWorld()
        {
            return Ok("Hello World");
        }
    }
}
