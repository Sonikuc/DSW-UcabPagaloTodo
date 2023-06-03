using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de la consulta que busca un servicio por su nombre y el nombre del proveedor que lo ofrece.
    /// </summary>
    public class ServiceByServiceNameHandler : IRequestHandler<ServiceByServiceNameQuery, OneServiceResponse>
	{
		private readonly IUCABPagaloTodoDbContext _dbContext;
		private readonly ILogger<ServiceByServiceNameHandler> _logger;


        /// <summary>
        /// Constructor de la clase ServiceByServiceNameHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public ServiceByServiceNameHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ServiceByServiceNameHandler> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}


        /// <summary>
        /// Manejador de la consulta que busca un servicio por su nombre y el nombre del proveedor que lo ofrece.
        /// </summary>
        /// <param name="request">La consulta ServiceByServiceNameQuery que especifica el nombre del servicio y el nombre del proveedor que lo ofrece.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto OneServiceResponse que contiene información detallada del servicio.</returns>
        public Task<OneServiceResponse> Handle(ServiceByServiceNameQuery request, CancellationToken cancellationToken)
		{
			try
			{
				if (request == null)
				{
					_logger.LogWarning("UserLoginQueryHandler.Handle: Request null.");

					throw new ArgumentNullException(nameof(request));

				}
				else
				{
					return HandleAsync(request);
				}
			}
			catch
			{
				_logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
				throw;
			}
			throw new NotImplementedException();
		}


        /// <summary>
        /// Método privado que maneja la búsqueda de un servicio por su nombre y el nombre del proveedor que lo ofrece.
        /// </summary>
        /// <param name="request">La consulta ServiceByServiceNameQuery que especifica el nombre del servicio y el nombre del proveedor que lo ofrece.</param>
        /// <returns>Un objeto OneServiceResponse que contiene información detallada del servicio.</returns>
        private async Task<OneServiceResponse> HandleAsync(ServiceByServiceNameQuery request)
		{
			var transaccion = _dbContext.BeginTransaction();
			try
			{
				_logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var user = await _dbContext.UserEntities.FirstOrDefaultAsync(c => c.Username == request._request.ProviderUsername);


                if (user == null)
                {
                    throw new InvalidOperationException("Update servicio fallido: el usuario no existe");
                }

                user.GetType();

                if (!(user is ProviderEntity provider))
                {
                    throw new InvalidOperationException("Update servicio fallido: el usuario no es un proveedor");
                }
                var service = _dbContext.ServiceEntities.FirstOrDefault(c => c.ServiceName == request._request.ServiceName && c.ProviderId == user.Id);
                //Count(c => c.ServiceName == request._request.ServiceName);

                if (service == null)
                {
                    throw new InvalidOperationException("Update servicio fallido: El proveedor no tiene este servicio");
                }

				var response = new OneServiceResponse
				{
					ServiceId = service.Id,
					ServiceName = service.ServiceName,
					TyperService = service.TypeService,
					ContactNumber = service.ContactNumber,
					ProviderUsername = service.Provider.Username,
					CompanyName = service.Provider.CompanyName
				};
				return response;
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
				throw;
			}
		}
	}
}
