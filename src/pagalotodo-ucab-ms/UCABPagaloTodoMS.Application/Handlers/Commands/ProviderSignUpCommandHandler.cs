using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para registrar un proveedor de servicios.
    /// </summary>
    public class ProviderSignUpCommandHandler : IRequestHandler<ProviderSignUpCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ProviderSignUpCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase ProviderSignUpCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para registrar el proveedor de servicios.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public ProviderSignUpCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ProviderSignUpCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para registrar un nuevo proveedor.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo ProviderSignUpCommand.</param>
        /// <param name="cancellationToken">El token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <returns>Una tarea que devuelve un identificador único (Guid) para el nuevo proveedor registrado.</returns>
        public async Task<Guid> Handle(ProviderSignUpCommand request, CancellationToken cancellationToken)
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


        // <summary>
        /// Método asincrónico que registra unnuevo proveedor.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo ProviderSignUpCommand.</param>
        /// <returns>Una tarea que devuelve un identificador único (Guid) para el nuevo proveedor registrado.</returns>
        private async Task<Guid> HandleAsync(ProviderSignUpCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                var result = _dbContext.UserEntities.Count(c => c.Username == request._request.Username);

                if (result > 0)
                {
                    throw new InvalidOperationException("Registro fallido: el usuario ya existe");
                }

                var entity = UserMapper.MapRequestToProviderEntity(request._request);
                _dbContext.UserEntities.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", id);
                return id;
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
