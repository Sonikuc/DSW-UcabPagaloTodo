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
    public class PaymentOptionController : BaseController<PaymentOptionController>
    {
        private readonly IMediator _mediator;
        public PaymentOptionController(ILogger<PaymentOptionController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para agregar una opción de pago a un servicio.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a un usuario agregar una opción de pago a un servicio a través de una solicitud HTTP POST.
        /// </remarks>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias para agregar la opción de pago.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información de la opción de pago agregada.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <response code="500">Se produjo un error interno en el servidor al procesar la solicitud.</response>
        /// <returns>Objeto JSON con la información de la opción de pago agregada.</returns>
        /// 

        [HttpPost("AddPaymentOption")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPaymentOption([FromBody] AddPaymentOptionRequest request)
        {
            _logger.LogInformation("Entrando al metodo que añade una opcion de pago a un servicio");
            try
            {
                var query = new AddPaymentOptionCommand(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: 0" + ex);
                throw;
            }
        }

        [HttpGet("PaymentOptionByServiceId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PaymentOptionsByServiceId([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al metodo que añade una opcion de pago a un servicio");
            try
            {
                var query = new PaymentOptionsByServiceIdQuery(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: 0" + ex);
                throw;
            }
        }
    }
}
