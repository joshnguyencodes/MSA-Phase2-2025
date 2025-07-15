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

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            
        }
    }
}