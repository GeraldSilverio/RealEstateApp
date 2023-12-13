using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Agent.Commands.ChangeStatus;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAll;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetById;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetRealEstateByIdAgent;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Agentes")]
    public class AgentController : BaseApiController
    {
        [Authorize(Roles = "Admin,Developer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Obtener todos los agentes",
            Description = "Devuelve el listado de todos los agentes creados en el sistema")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllAgentQuery()));
        }

        [Authorize(Roles = "Admin,Developer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Obtener un agente por su ID",
            Description = "Busca y devuelve un agente por su ID")]
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(GetAgentByIdQuery request)
        {
            return Ok(await Mediator.Send(new GetAgentByIdQuery { IdAgent = request.IdAgent }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Cambiar el estado de un agente",
            Description = "Recibe los parametros necesarios para cambiar el estado del agente")]
        public async Task<IActionResult> ChangeUpdate(ChangeStatusAgentCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("GetRealEstateByAgentId")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Obtener las propiedades de su agente ",
            Description = "Devuelve el listado de las propiedades de ese agente por ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRealEstateByAgentId(GetRealEstateByAgentIdQuery request)
        {

            return Ok(await Mediator.Send(new GetRealEstateByAgentIdQuery { IdAgent = request.IdAgent }));
        }


    }
}
