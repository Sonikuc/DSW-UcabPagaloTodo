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
    public class ServiceDeleteController : BaseController<ServiceDeleteController>
    {
        private readonly IMediator _mediator;

        public ServiceDeleteController(ILogger<ServiceDeleteController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para la eliminación de un servicio.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con la propiedad "Id" que contiene el identificador único del servicio a eliminar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que el servicio ha sido eliminado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpPost("DeleteService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromBody] ServiceDeleteRequest request)
        {
            _logger.LogInformation("Entrando al metodo que elimina un servicio");
            try
            {
                var query = new ServiceDeleteCommand(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la actualizacion de datos del usuario. Exception: " + ex);
                throw;
            }
        }
    }
}
