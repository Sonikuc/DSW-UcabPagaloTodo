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
    public class LoginController : BaseController<LoginController>
    {
        private readonly IMediator _mediator;

        public LoginController(ILogger<LoginController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Endpoint para autenticar a un usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a los usuarios iniciar sesión en el sistema a través de una solicitud HTTP POST.
        /// </remarks>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con dos propiedades: UserName y Password.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información del usuario autenticado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <returns>Objeto JSON con la información del usuario autenticado.</returns>
        /// 
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest request)
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
            try
            {
                
                var query = new UserLoginQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response); 
               
            }
           
            catch (ArgumentException ex)
            { 
               return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }
    }
}
