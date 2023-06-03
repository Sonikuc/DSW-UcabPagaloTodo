using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de consulta que devuelve una lista de todos los usuarios registrados en el sistema.
    /// </summary>
    internal class AllUserQueryHandler : IRequestHandler<AllUserQuery, List<AllUserQueryResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AllUserQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase AllUserQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar los usuarios.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllUserQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AllUserQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de consulta que devuelve una lista de todos los usuarios registrados en el sistema.
        /// </summary>
        /// <param name="request">La consulta AllUserQuery que especifica los criterios de búsqueda de los usuarios.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos AllUserQueryResponse que contienen información detallada de los usuarios.</returns>
        public Task<List<AllUserQueryResponse>> Handle(AllUserQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de todos los usuarios en la base de datos.
        /// </summary>
        /// <param name="request">La consulta AllUserQuery que especifica los criterios de búsqueda delos usuarios.</param>
        /// <returns>Una lista de objetos AllUserQueryResponse que contienen información detallada de los usuarios.</returns>
        private async Task<List<AllUserQueryResponse>> HandleAsync(AllUserQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = _dbContext.UserEntities.Select(c => new AllUserQueryResponse()
                {
                   Dni = c.Dni,
                   Name = c.Name,
                   Lastname = c.Lastname,
                   Username = c.Username,
                   Email = c.Email,
                   PhoneNumber = c.PhoneNumber

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
