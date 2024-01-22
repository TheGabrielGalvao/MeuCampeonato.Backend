using Domain.DTO;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{uuid}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid uuid)
        {
            try
            {
                var user = await _userService.GetByIdAsync(uuid);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UserRequest user)
        {
            try
            {
                var createdUser = await _userService.AddAsync(user);
                return CreatedAtAction(nameof(Get), new { uuid = createdUser.Uuid }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest user)
        {
            try
            {
                var userExists = _userService.GetFullUserInfo(user.UserName);
                if (userExists.Result == null)
                {
                    var createdUser = await _userService.AddAsync(user);
                    return CreatedAtAction(nameof(Get), new { uuid = createdUser.Uuid }, createdUser);
                }
                else
                {
                    return Ok("This Username already exists!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{uuid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid uuid, [FromBody] UserRequest user)
        {
            try
            {
                var updatedUser = await _userService.UpdateAsync(uuid, user);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{uuid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid uuid)
        {
            try
            {
                var user = await _userService.GetByIdAsync(uuid);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.DeleteAsync(uuid);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("full-info")]
        [Authorize]
        public async Task<IActionResult> GetFullUserInfo()
        {
            try
            {
                var username = User.Identity?.Name; // Obtém o nome do usuário logado
                var userInfo = await _userService.GetFullUserInfo(username);

                if (userInfo == null)
                {
                    return NotFound();
                }

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
