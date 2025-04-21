using OAuthTest.Dtos;
using OAuthTest.Dtos.Form;
using OAuthTest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuthTest.Controllers
{
    [Route("v1/customer")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<AuthResultDto> Register([FromBody] RegisterForm form) => await _customerService.Register(form);

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<AuthResultDto> Login([FromBody] LoginForm form) => await _customerService.Login(form);

        [HttpGet("profile")]
        public async Task<CustomerDto> GetProfile() => await _customerService.GetProfile();

        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<string> Test() => "This is a test";
    }
}
