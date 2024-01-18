using Domain.DTO;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Authorize]
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
                var createdCustomer = await _userService.AddAsync(user);
                return CreatedAtAction(nameof(Get), new { uuid = createdCustomer.Uuid }, createdCustomer);
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
                var updatedCustomer = await _userService.UpdateAsync(uuid, user);
                if (updatedCustomer == null)
                {
                    return NotFound();
                }
                return Ok(updatedCustomer);
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
    }
}
