using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para agregar una opción de pago.
    /// </summary>

    public class AddPaymentOptionCommandHandler : IRequestHandler<AddPaymentOptionCommand, AddPaymentOptionResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddPaymentOptionCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase AddPaymentOptionCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para agregar la opción de pago.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public AddPaymentOptionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddPaymentOptionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para agregar una opción de pago a un servicio.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddPaymentOptionCommand.</param>
        /// <param name="cancellationToken">Token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si el servicio no existe.</exception>
        /// <returns>Un objeto AddPaymentOptionResponse que indica si la opción de pago se ha agregado correctamente.</returns>
        public async Task<AddPaymentOptionResponse> Handle(AddPaymentOptionCommand request, CancellationToken cancellationToken)
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
        /// Método asincrónico que agrega una opción de pago a un servicio.
        /// </summary>
        ////// <param name="request">Objeto de comando de tipo AddPaymentOptionCommand.</param>
        /// <exception cref="InvalidOperationException">Se lanza si el servicio no existe.</exception>
        /// <returns>Un objeto AddPaymentOptionResponse que indica si la opción de pago se ha agregado correctamente.</returns>
        private async Task<AddPaymentOptionResponse> HandleAsync(AddPaymentOptionCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                var service = _dbContext.ServiceEntities.FirstOrDefault(s => s.Id == request._request.ServiceId);

                if (service == null)
                {
                    throw new InvalidOperationException("Registrar opcion de pago fallido: No existe el servicio");
                }
                var option = new PaymentOptionEntity
                {
                    Name = request._request.Name,
                    Status = request._request.Status,
                    ServiceId = request._request.ServiceId
                };

                _dbContext.PaymentOptionEntities.Add(option);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                var response = new AddPaymentOptionResponse
                {
                    success = true,
                    message = "Metodo de pago registrado con exito"
                };
                _logger.LogInformation("AddPaymentOptionCommandHandler.HandleAsync {Response}", response);
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
