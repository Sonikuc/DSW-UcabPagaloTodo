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
    /// Manejador de la consulta que busca un servicio por su identificador único.
    /// </summary>
    internal class ServiceByGuidQueryHandler : IRequestHandler<ServiceByGuidQuery, OneServiceResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ServiceByServiceNameHandler> _logger;


        /// <summary>
        /// Constructor de la clase ServiceByGuidQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public ServiceByGuidQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ServiceByServiceNameHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca un servicio por su identificador único.
        /// </summary>
        /// <param name="request">La consulta ServiceByGuidQuery que especifica el identificador único del servicio para buscar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto OneServiceResponse que contiene información detallada del servicio.</returns>
        public Task<OneServiceResponse> Handle(ServiceByGuidQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de un servicio por su identificador único.
        /// </summary>
        /// <param name="request">La consulta ServiceByGuidQuery que especifica el identificador único del servicio para buscar.</param>
        /// <returns>Un objeto OneServiceResponse que contiene información detallada del servicio.</returns>
        private async Task<OneServiceResponse> HandleAsync(ServiceByGuidQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var service = await _dbContext.ServiceEntities.FirstOrDefaultAsync(c => c.Id == request.ServiceId);

                if (service == null)
                {
                    throw new InvalidOperationException("Update servicio fallido: El proveedor no tiene este servicio");
                }

                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Id == service.ProviderId);

                if (user == null) {
                    throw new InvalidOperationException("Update servicio fallido: El proveedor no tiene este servicio");
                }
 
                if (!(user is ProviderEntity provider)) {
                    throw new InvalidOperationException("Update servicio fallido: El proveedor no tiene este servicio");
                }

                var response = new OneServiceResponse
                {
                    ServiceId = service.Id,
                    ServiceName = service.ServiceName,
                    TyperService = service.TypeService,
                    ContactNumber = service.ContactNumber,
                    ProviderUsername = provider.Username,
                    CompanyName = provider.CompanyName
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
