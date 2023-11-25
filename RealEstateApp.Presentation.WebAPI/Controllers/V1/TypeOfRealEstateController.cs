using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TypeOfRealEstateController : BaseApiController
    {
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;

        public TypeOfRealEstateController(ITypeOfRealEstateService typeOfRealEstateService)
        {
            _typeOfRealEstateService = typeOfRealEstateService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveTypeOfEstateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveTypeOfEstateDto typeOfEstateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Debe enviar los datos correctamente");
                }
                var response = await _typeOfRealEstateService.Add(typeOfEstateDto);
                return StatusCode(StatusCodes.Status201Created, "Tipo de Propiedad creado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var types = await _typeOfRealEstateService.GetAll();

                if (types.Count == 0)
                {
                    return NoContent();
                }
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var type = await _typeOfRealEstateService.GetById(id);

                if (type is null)
                {
                    return NoContent();
                }
                return Ok(type);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(SaveTypeOfEstateDto typeOfEstateDto, int id)
        {
            try
            {
                typeOfEstateDto.Id = id;
                if (!ModelState.IsValid)
                {
                    return BadRequest("Debe enviar los datos correctamente");
                }
                await _typeOfRealEstateService.Update(typeOfEstateDto, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _typeOfRealEstateService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
