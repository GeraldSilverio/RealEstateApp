﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements;
using RealEstateApp.Core.Application.Features.RealState.Queries.GetAllRealState;
using RealEstateApp.Core.Application.Features.RealState.Queries.GetRealStateByCode;
using RealEstateApp.Core.Application.Features.RealState.Queries.GetRealStateById;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class RealStateController : BaseApiController
    {

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de propiedades",
            Description = "Obtiene el listado de todas las propiedades creadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllRealEstateQuery()));
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Propiedad por id.",
            Description = "Obtiene una propiedad filtrada por id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById()
        {
            return Ok(await Mediator.Send(new GetRealEstateByIdQuery()));
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Propiedad por codigo.",
            Description = "Obtiene una propiedad filtrada por codigo.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode()
        {
            return Ok(await Mediator.Send(new GetRealEstateByCodeQuery()));
        }
    }
}
