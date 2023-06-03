using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;

namespace UCABPagaloTodoMS.Controllers
{
    //Controlador que maneja los metodos de actualizacion de algun atributo del usuario
    //Y tambien maneja el metodo de cambiar estado del usuario (activo o inactivo)
    [ApiController]
    [Route("api/[controller]")]
    public class UserUpdateController : BaseController<UserUpdateController>
    {
        private readonly IMediator _mediator;

        public UserUpdateController(ILogger<UserUpdateController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para actualizar los datos de un usuario.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades del usuario a actualizar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que el usuario ha sido actualizado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 
        [HttpPost("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> update([FromBody] UserUpdateRequest request)
        {
            _logger.LogInformation("Entrando al metodo que actualiza datos del usuario");
            try
            {
                var query = new UserUpdateCommand(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la actualizacion de datos del usuario. Exception: " + ex);
                throw;
            }
        }
        /// <summary>
        /// Endpoint para actualizar los datos de un usuario.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades del usuario a actualizar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que el usuario ha sido actualizado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>

        [HttpPost("ChangeUserStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeUserStatus([FromBody] ChangeUserStatusRequest request)
        {
            _logger.LogInformation("Entrando al metodo que actualiza datos del usuario");
            try
            {
                var query = new ChangeUserStatusCommand(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en el cambio de estado del usuario. Exception: " + ex);
                throw;
            }
        }
    }
}
