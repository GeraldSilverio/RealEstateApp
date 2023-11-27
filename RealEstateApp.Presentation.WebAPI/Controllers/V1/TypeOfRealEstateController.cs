using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.CreateTypeOfRealEstate;
using RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.DeleteTypeOfRealEstateById;
using RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.UpdateTypeOfRealEstate;
using RealEstateApp.Core.Application.Features.TypeOfRealEstates.Queries.GetAllTypeOfRealEstate;
using RealEstateApp.Core.Application.Features.TypeOfRealEstates.Queries.GetTypeOfRealEstateById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de tipos de propiedades")]
    public class TypeOfRealEstateController : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creacion de un tipo de propiedad",
            Description = "Recibe los parametros que necesita para crear un tipo de propiedad")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateTypeOfRealEstateCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateTypeOfRealEstateCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Debe enviar los datos correctamente");
                }
               await Mediator.Send(command);
               return StatusCode(StatusCodes.Status201Created, "Tipo de Propiedad creado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de tipos de propiedades",
            Description = "Obtiene el listado de todos los tipos de propiedades creados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                return Ok(await Mediator.Send(new GetAllTypeOfRealEstateQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Tipo de propiedad por id",
            Description = "Obtiene un tipo de propiedad filtrado por su id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetTypeOfRealEstateByIdQuery { Id = id}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizacion de un tipo de propiedad",
            Description = "Recibe los paramentros necesarios para modificar un tipo de propiedad existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateTypeOfRealEstateCommand command, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Debe enviar los datos correctamente");
                }
                if (command.Id != id)
                {
                    return BadRequest("Debe enviar los datos correctamente");

                }
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Eliminar un tipo de propiedad",
            Description = "Recibe los parametros necesarios para eliminar un tipo de propiedad existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTypeOfRealEstateByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
