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
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de consulta que devuelve una lista de todos los proveedores registrados en el sistema.
    /// </summary
    internal class AllProvidersQueryHandler : IRequestHandler<AllProvidersQuery, List<AllProvidersResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AllProvidersQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase AllProvidersQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar los proveedores.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllProvidersQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AllProvidersQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de consulta que devuelve una lista de todos los proveedores registrados en el sistema.
        /// </summary>
        /// <param name="request">La consulta AllProvidersQuery que especifica los criterios de búsqueda de los proveedores.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos AllProvidersResponse que contienen información detallada de los proveedores.</returns>
        public Task<List<AllProvidersResponse>> Handle(AllProvidersQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de todos los proveedores en la base de datos.
        /// </summary>
        /// <param name="request">La consulta AllProvidersQuery que especifica los criterios de búsqueda de los proveedores.</param>
        /// <returns>Una lista deobjetos AllProvidersResponse que contienen información detallada de los proveedores.</returns>
        private async Task<List<AllProvidersResponse>> HandleAsync(AllProvidersQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = _dbContext.UserEntities.Where(c=> c is ProviderEntity).Select(c => new AllProvidersResponse()
                {
                    Dni = c.Dni,
                    Name = c.Name,
                    Lastname = c.Lastname,
                    Username = c.Username,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
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

