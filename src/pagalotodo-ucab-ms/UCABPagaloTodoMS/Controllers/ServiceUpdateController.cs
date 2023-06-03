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
    public class ServiceUpdateController : BaseController<ServiceUpdateController>
    {
        private readonly IMediator _mediator;

        public ServiceUpdateController(ILogger<ServiceUpdateController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para la actualización de datos de un servicio.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades del servicio a actualizar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que el servicio ha sido actualizado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 
        [HttpPut("UpdateService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> update([FromBody] ServiceUpdateRequest request)
        {
            _logger.LogInformation("Entrando al metodo que actualiza datos del servicio");
            try
            {
                var query = new ServiceUpdateCommand(request);
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
