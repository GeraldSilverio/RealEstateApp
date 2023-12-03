using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Features.Improvements.Commands.CreateImprovement;
using RealEstateApp.Core.Application.Features.Improvements.Commands.DeleteImprovementById;
using RealEstateApp.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using RealEstateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements;
using RealEstateApp.Core.Application.Features.Improvements.Queries.GetImprovementsById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]
    public class ImprovementController : BaseApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creacion de una mejora",
            Description = "Recibe los parametros que necesita para crear una mejora")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveImprovementDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateImprovementCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            var response = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, "Mejora creada correctamente");
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de mejoras",
            Description = "Obtiene el listado de todas las mejoras creadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
             return Ok(await Mediator.Send(new GetAllImprovementsQuery()));
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Mejora por id",
            Description = "Obtiene una mejora filtrada por su id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
             return Ok(await Mediator.Send(new GetImprovementByIdQuery { Id = id }));           
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizacion de una mejora",
            Description = "Recibe los paramentros necesarios para modificar una mejora existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateImprovementCommand command, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            if(command.Id != id)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Eliminar una mejorar",
            Description = "Recibe los parametros necesarios para eliminar una mejora existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteImprovementByIdCommand { Id = id });
            return NoContent();
        }

    }
}
