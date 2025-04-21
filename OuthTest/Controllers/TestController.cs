using Microsoft.AspNetCore.Mvc;

namespace OAuthTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("[action]")]
        public string SayHello() => "Hello";
    }
}
