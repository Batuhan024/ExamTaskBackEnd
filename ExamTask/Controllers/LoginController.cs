using Business.Models.Request.Functional;
using Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            var response = _authService.Authenticate(request);

            if (response == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            return Ok(response);
        }
    }
}
