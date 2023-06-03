using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : BaseController<SignUpController>
    {
        private readonly IMediator _mediator;

        public SignUpController(ILogger<SignUpController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para el registro de un nuevo usuario.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades del nuevo usuario a registrar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que el usuario ha sido registrado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpPost("signupuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> signupuser([FromBody] UserSignUpRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra un nuevo usuario");
            try
            {
                var command = new UserSignUpCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        /// <summary>
        /// Endpoint para el registro de un nuevo proveedor.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades del nuevo proveedor a registrar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que el proveedor ha sido registrado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpPost("signupprovider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> signupprovider([FromBody] ProviderSignUpRequest request)
        {
            _logger.LogInformation("Entrando al metodo que registra un nuevo usuario");
            try
            {
                var command = new ProviderSignUpCommand(request);
                var response = await _mediator.Send(command);
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
