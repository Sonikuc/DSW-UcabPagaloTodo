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
    /// Manejador de la consulta que busca opciones de pago por ID de servicio.
    /// </summary>
    internal class PaymentOptionsByServiceIdQueryHandler : IRequestHandler<PaymentOptionsByServiceIdQuery, List<PaymentOptionsByServiceIdResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<PaymentOptionsByServiceIdQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase PaymentOptionsByServiceIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar las opciones de pago.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public PaymentOptionsByServiceIdQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<PaymentOptionsByServiceIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca opciones de pago por ID de servicio.
        /// </summary>
        /// <param name="request">La consulta PaymentOptionsByServiceIdQuery que especifica el ID del servicio para buscar las opciones de pago.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos PaymentOptionsByServiceIdResponse que contienen información detallada de las opciones de pago.</returns>
        public Task<List<PaymentOptionsByServiceIdResponse>> Handle(PaymentOptionsByServiceIdQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de opciones de pago por ID de servicio.
        /// </summary>
        /// <param name="request">La consulta PaymentOptionsByServiceIdQuery que especifica el ID del servicio para buscar las opciones de pago.</param>
        /// <returns>Una lista de objetos PaymentOptionsByServiceIdResponse que contienen información detallada de las opciones de pago.</returns>
        private async Task<List<PaymentOptionsByServiceIdResponse>> HandleAsync(PaymentOptionsByServiceIdQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var option = _dbContext.PaymentOptionEntities.Where(c => c.ServiceId == request.ServiceId && c.Status == "Activo").Select(c => new PaymentOptionsByServiceIdResponse() 
                {
                    PaymentOptionId = c.Id,
                    PaymentOptionName = c.Name,
                    Status = c.Status
                });

                return await option.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
