using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para actualizar un servicio registrado.
    /// </summary>
    public class ServiceUpdateCommandHandler : IRequestHandler<ServiceUpdateCommand, ServiceUpdateResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ServiceUpdateCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase ServiceUpdateCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para actualizar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public ServiceUpdateCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ServiceUpdateCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para actualizar un servicio registrado.
        /// </summary>
        /// <param name="request">El comando ServiceUpdateCommand que contiene la información del servicio a actualizar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto ServiceUpdateResponse que indica si la actualización del servicio fue exitosa.</returns>
        public async Task<ServiceUpdateResponse> Handle(ServiceUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Método privado que maneja la actualización del servicio registrado.
        /// </summary>
        /// <param name="request">El comando ServiceUpdateCommand que contiene la información del servicio a actualizar.</param>
        /// <returns>Un objeto ServiceUpdateResponse que indica si la actualización del servicio fue exitosa.</returns>
        private async Task<ServiceUpdateResponse> HandleAsync(ServiceUpdateCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("ServiceUpdateCommandHandler.HandleAsync {Request}", request);
                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Username == request._request.UserName);


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

                var properties = request._request.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var value = property.GetValue(request._request);
                    if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    {
                        var userProperty = service.GetType().GetProperty(property.Name);
                        if (userProperty != null && userProperty.CanWrite)
                        {
                            userProperty.SetValue(service, value);
                        }
                    }
                }


                _dbContext.ServiceEntities.Update(service);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                var response = new ServiceUpdateResponse
                {
                    ServiceName = service.ServiceName,
                    ContactNumber = service.ContactNumber
                };
                _logger.LogInformation("ServiceUpdateCommandHandler.HandleAsync {Response}", response);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
