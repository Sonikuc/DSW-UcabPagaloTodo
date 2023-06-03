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
    /// Manejador de consulta que devuelve una lista de todas las facturas.
    /// </summary>
    public class AllBillsQueryHandler : IRequestHandler<AllBillsQuery, List<AllBillsQueryResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AllBillsQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase AllBillsQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar las facturas.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllBillsQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AllBillsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de consulta que devuelve una lista de todas las facturas.
        /// </summary>
        /// <param name="request">La consulta AllBillsQuery que especifica los criterios de búsqueda de las facturas.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos AllBillsQueryResponse que contienen información detallada de las facturas.</returns>
        public Task<List<AllBillsQueryResponse>> Handle(AllBillsQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de todas las facturas en la base de datos.
        /// </summary>
        /// <param name="request">La consulta AllBillsQuery que especifica los criterios de búsqueda de las facturas.</param>
        /// <returns>Una lista de objetos AllBillsQueryResponseque contienen información detallada de las facturas.</returns>
        private async Task<List<AllBillsQueryResponse>> HandleAsync(AllBillsQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = _dbContext.BillEntities.Select(c => new AllBillsQueryResponse()
                {
                    ContractNumber = c.ContractNumber,
                    PhoneNumber = c.PhoneNumber,
                    Amount= c.Amount,
                    Date = c.Date,
                    UserId = c.UserId,
                    UserName = c.User.Username,
                    ServiceId = c.ServiceId,
                    ServiceName = c.Service.ServiceName
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
