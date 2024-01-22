using Domain.DTO;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var authResult = await _authService.ExecuteAuth(request);

            if (authResult.Success)
            {
                return Ok(new { Token = authResult.AccessToken });
            }

            return Unauthorized();
        }

        [HttpGet("VerifyToken")]
        [Authorize]
        public async Task<IActionResult> VerifyToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var isValid = await _authService.CheckToken(token);

            if (isValid)
            {
                return Ok("Token is valid.");
            }
            else
            {
                return Unauthorized("Token is invalid or expired.");
            }
        }

    }
}
