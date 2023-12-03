using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Features.TypeOfSales.Command.CreateTypeOfSale;
using RealEstateApp.Core.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById;
using RealEstateApp.Core.Application.Features.TypeOfSales.Command.UpdateTypeOfSale;
using RealEstateApp.Core.Application.Features.TypeOfSales.Queries.GetAllTypeOfSale;
using RealEstateApp.Core.Application.Features.TypeOfSales.Queries.GetAllTypeOfSaleById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de tipos de ventas")]
    public class TypeOfSaleController : BaseApiController
    {
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveTypeOfSaleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creacion de un tipo de venta",
            Description = "Recibe los parametros que necesita para crear un tipo de venta")]
        public async Task<IActionResult> Post(CreateTypeOfSaleCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, "Tipo de Venta creado correctamente");   
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de tipos de ventas",
            Description = "Obtiene el listado de todos los tipos de ventas creados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllTypeOfSaleQuery()));
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Tipo de venta por id",
            Description = "Obtiene un tipo de venta filtrado por su id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTypeOfSaleByIdQuery { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizacion de un tipo de ventas",
            Description = "Recibe los paramentros necesarios para modificar un tipo de venta existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateTypeOfSaleCommand command, int id)
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Eliminar un tipo de venta",
            Description = "Recibe los parametros necesarios para eliminar un tipo de venta existente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTypeOfSaleByIdCommand { Id = id});
            return NoContent();           
        }
    }
}
