using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceQueryController : BaseController<ServiceQueryController>
    {
        private readonly IMediator _mediator;

        public ServiceQueryController(ILogger<ServiceQueryController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Endpoint para la consulta de todos los servicios.
        /// </summary>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene todos los servicios.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpGet("AllServices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllServices()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var query = new AllServicesQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

		[HttpPost("ByServiceName")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> byServiceName([FromBody] OneServiceRequest ServiceName)
		{
			_logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new ServiceByServiceNameQuery(ServiceName);
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
        /// Endpoint para la consulta de un servicio por identificador único.
        /// </summary>
        /// <param name="id">Identificador único del servicio a consultar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene el servicio consultado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpGet("ByGuid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> byGuid([FromQuery] Guid id)
        {
            _logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new ServiceByGuidQuery(id);
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