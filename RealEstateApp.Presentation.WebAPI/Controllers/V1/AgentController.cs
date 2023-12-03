using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AgentController : BaseApiController
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var agents = await _agentService.GetAllAgentAsync();
            if (agents.Count == 0)
            {
                return NoContent();
            }
            return Ok(agents);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var agent = await _agentService.GetAgentByIdAsync(id);
            if (agent is null)
            {
                return NoContent();
            }
            return Ok(agent);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ChangeUpdate(string id, bool status)
        {
            await _agentService.ChangeStatus(id, status);
            return NoContent();
        }

    }
}
