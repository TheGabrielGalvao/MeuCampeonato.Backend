using AutoMapper;
using Domain.DTO;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teams = await _teamService.GetAllAsync();
                return Ok(teams);
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
                var team = await _teamService.GetByIdAsync(uuid);
                if (team == null)
                {
                    return NotFound();
                }
                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] TeamRequest team)
        {
            try
            {
                var createdTeam = await _teamService.AddAsync(team);
                return CreatedAtAction(nameof(Get), new { uuid = createdTeam.Uuid }, createdTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{uuid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid uuid, [FromBody] TeamRequest team)
        {
            try
            {
                var updatedTeam = await _teamService.UpdateAsync(uuid, team);
                if (updatedTeam == null)
                {
                    return NotFound();
                }
                return Ok(updatedTeam);
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
                var team = await _teamService.GetByIdAsync(uuid);
                if (team == null)
                {
                    return NotFound();
                }

                await _teamService.DeleteAsync(uuid);
                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("option-items")]
        [Authorize]
        public async Task<IEnumerable<OptionItemResponse>> GetTeamsToSelectOption()
        {
            return _mapper.Map<List<OptionItemResponse>>(await _teamService.GetAllAsync());
        }
    }
}
