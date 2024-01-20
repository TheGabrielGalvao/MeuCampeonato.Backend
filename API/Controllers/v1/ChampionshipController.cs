using AutoMapper;
using Domain.DTO;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChampionshipController : ControllerBase
    {
        private readonly IChampionshipService _championshipService;
        private readonly IMapper _mapper;
        public ChampionshipController(IChampionshipService championshipService, IMapper mapper)
        {
            _championshipService = championshipService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var championships = await _championshipService.GetAllAsync();
                return Ok(championships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{uuid}")]
        public async Task<IActionResult> Get(Guid uuid)
        {
            try
            {
                var championship = await _championshipService.GetByIdAsync(uuid);
                if (championship == null)
                {
                    return NotFound();
                }
                return Ok(championship);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChampionshipRequest championship)
        {
            try
            {
                var createdChampionship = await _championshipService.AddAsync(championship);
                return CreatedAtAction(nameof(Get), new { uuid = createdChampionship.Uuid }, createdChampionship);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
