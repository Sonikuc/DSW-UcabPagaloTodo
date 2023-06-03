using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de consulta que devuelve una lista de todos los servicios registrados en el sistema.
    /// </summary>
    internal class AllServicesQueryHandler : IRequestHandler<AllServicesQuery, List<AllServicesQueryResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AllServicesQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase AllServicesQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar los servicios.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllServicesQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AllServicesQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de consulta que devuelve una lista de todos los servicios registrados en el sistema.
        /// </summary>
        /// <param name="request">La consulta AllServicesQuery que especifica los criterios de búsqueda de los servicios.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos AllServicesQueryResponse que contienen información detallada de los servicios.</returns>
        public Task<List<AllServicesQueryResponse>> Handle(AllServicesQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de todos los servicios en la base de datos.
        /// </summary>
        /// <param name="request">La consulta AllServicesQuery que especifica los criterios de búsqueda de los servicios.</param>
        /// <returns>Una lista de objetos AllServicesQueryResponse que contieneninformación detallada de los servicios.</returns>
        private async Task<List<AllServicesQueryResponse>> HandleAsync(AllServicesQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = _dbContext.ServiceEntities.Select(c => new AllServicesQueryResponse()
                {
                    ServiceId = c.Id,
                    ServiceName = c.ServiceName,
                    ContactNumber = c.ContactNumber,
                    TyperService = c.TypeService,
                    ProviderUsername = c.Provider.Username,
                    CompanyName = c.Provider.CompanyName

                });

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
