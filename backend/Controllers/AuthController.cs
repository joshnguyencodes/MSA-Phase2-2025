using Microsoft.AspNetCore.Mvc;
using bulkbuy.api.Models;
using bulkbuy.api.Services;
using System.Threading.Tasks;
namespace bulkbuy.api.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _authService.RegisterUser(user);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { Message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authService.LoginUser(loginRequest);
            if (!result.Success)
            {
                return Unauthorized(new { Message = result.Message });
            }
            return Ok(new { Token = result.Token });
        }
    }
}