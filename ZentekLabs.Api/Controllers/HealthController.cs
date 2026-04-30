using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZentekLabs.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class HealthController : ControllerBase
    {

        [HttpGet("Check")]  
        public IActionResult GetHealth()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}
