using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserQueryController : BaseController<UserQueryController>
    {
        private readonly IMediator _mediator;

        public UserQueryController(ILogger<UserQueryController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para la consulta de todos los usuarios registrados.
        /// </summary>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene todos los usuarios registrados.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpGet("AllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllUsers()
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
            try
            {
                var query = new AllUserQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        /// <summary>
        /// Endpoint para la consulta de un usuario por su nombre de usuario.
        /// </summary>
        /// <param name= "username">Nombre de usuario del usuario a consultar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene el usuario consultado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 
        [HttpGet("ByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> byUsername([FromQuery] string username)
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
            try
            {
                var query = new UserByUsernameQuery(username);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        /// <summary>
        /// Endpoint para la consulta de un usuario por su número de DNI.
        /// </summary>
        /// <param name="Dni">Número de DNI del usuario a consultar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene el usuario consultado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>

        [HttpGet("ByDni")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> byDni([FromQuery] string Dni)
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
            try
            {
                var query = new UserByDNIQuery(Dni);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        [HttpGet("AllProviders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllProviders()
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
            try
            {
                var query = new AllProvidersQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

    }
}
