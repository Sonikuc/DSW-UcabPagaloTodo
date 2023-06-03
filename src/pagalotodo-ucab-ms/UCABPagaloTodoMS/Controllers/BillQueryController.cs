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
    public class BillQueryController : BaseController<BillQueryController>
    {
        private readonly IMediator _mediator;

        public BillQueryController(ILogger<BillQueryController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Endpoint para obtener todas las facturas de servicios.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite obtener todas las facturas de servicios registradas en el sistema a través de una solicitud HTTP GET.
        /// </remarks>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información de todas las facturas de servicios.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <returns>Objeto JSON con la información de todas las facturas de servicios.</returns>

        [HttpGet("AllBills")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllBills()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var query = new AllBillsQuery();
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
        /// Endpoint para obtener las facturas de servicios de un usuario específico.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite obtener las facturas de servicios registradas en el sistema para un usuario específico a través de una solicitud HTTP GET.
        /// </remarks>
        /// <param name="UserId">Identificador único del usuario del cual se quieren obtener las facturas de servicios.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información de las facturas de servicios del usuario.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <returns>Objeto JSON con la información de las facturas de servicios del usuario.</returns>

        [HttpGet("ByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> byUserId([FromQuery] Guid UserId)
        {
            _logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new BillByUserIdQuery(UserId);
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
        /// Endpoint para obtener las facturas de servicios de un servicio específico.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite obtener las facturas de servicios registradas en el sistema para un servicio específico a través de una solicitud HTTP GET.
        /// </remarks>
        /// <param name="ServiceId">Identificador único del servicio del cual se quieren obtener las facturas de servicios.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información de las facturas de servicios del servicio.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <returns>Objeto JSON con la información de las facturas de servicios del servicio.</returns>

        [HttpGet("ByServiceId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> byServiceId([FromQuery] Guid ServiceId)
        {
            _logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new BillByServiceIdQuery(ServiceId);
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
