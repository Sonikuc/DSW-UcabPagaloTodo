using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para eliminar un servicio registrado.
    /// </summary>
    internal class ServiceDeleteCommandHandler : IRequestHandler<ServiceDeleteCommand, ServiceDeleteResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ServiceDeleteCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase ServiceDeleteCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para eliminar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public ServiceDeleteCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ServiceDeleteCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para eliminar un servicio registrado.
        /// </summary>
        /// <param name="request">El comando ServiceDeleteCommand que contiene la información del servicio a eliminar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto ServiceDeleteResponse que indica si la eliminación del servicio fue exitosa.</returns>
        public async Task<ServiceDeleteResponse> Handle(ServiceDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ServiceDeleteCommandHandler.Handle: Request nulo.");
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
        /// Método privado que maneja la eliminación del servicio registrado.
        /// </summary>
        /// <param name="request">El comando ServiceDeleteCommand que contiene la información del servicio a eliminar.</param>
        /// <returns>Un objeto ServiceDeleteResponse que indica si la eliminación del servicio fue exitosa.</returns>
        private async Task<ServiceDeleteResponse> HandleAsync(ServiceDeleteCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("ServiceDeleteCommandHandler.HandleAsync {Request}", request);
                var service = _dbContext.ServiceEntities
                    .Where(s => s.ServiceName == request._request.ServiceName && s.Provider.Username == request._request.UserName)
                    .FirstOrDefault();

                if (service == null)
                {
                    throw new InvalidOperationException("Delete fallido: el servicio no existe o el usuario no tiene este servicio");
                }

                _dbContext.ServiceEntities.Remove(service);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                var response = new ServiceDeleteResponse
                {
                    Message = ("Servicio eliminado exitosamente")
                };
                _logger.LogInformation("ServiceDeleteCommandHandler.HandleAsync {Response}", response);
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
