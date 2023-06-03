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
	public class AddServiceController : BaseController<AddServiceController>				
	{
		private readonly IMediator _mediator;
		public AddServiceController(ILogger<AddServiceController> logger, IMediator mediator) : base(logger)
		{
			_mediator = mediator;
		}

        /// <summary>
        /// Método que agrega un servicio al sistema.
        /// </summary>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias para agregar el servicio.</param>
        /// <returns>Un objeto JSON con la información del servicio agregado.</returns>
        /// <remarks>
        /// Este método recibe una solicitud HTTP POST con un objeto JSON en el cuerpo de la solicitud que contiene las propiedades necesarias para agregar un servicio al sistema.
        /// El método devuelve un objeto JSON con la información del servicio agregado.
        /// </remarks>
		
        [HttpPost("AddService")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddService([FromBody] AddServiceRequest request)
		{
			_logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
			try
			{
				var query = new AddServiceCommand(request);
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
