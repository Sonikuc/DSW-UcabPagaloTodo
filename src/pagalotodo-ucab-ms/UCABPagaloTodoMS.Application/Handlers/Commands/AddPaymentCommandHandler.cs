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
    /// Manejador de comando para agregar un pago.
    /// </summary>
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, AddPaymentResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddPaymentCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase AddPaymentCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para agregar el pago.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public AddPaymentCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddPaymentCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para agregar un pago a una factura de servicio.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddPaymentCommand.</param>
        /// <param name="cancellationToken">Token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario que hizo el pago no existe, si el usuario no es un usuario consumidor, si el servicio no existe, si la opción de pago no existe o si el método de pago no está activo.</exception>
        /// <returns>Un objeto AddPaymentResponse que indica si el pago se ha agregado correctamente.</returns>
        public async Task<AddPaymentResponse> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
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
        /// Método asincrónico que agrega un pago a una factura de servicio.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddPaymentCommand.</param>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario que hizo el pago no existe, si el usuario no es un usuario consumidor, si el servicio no existe, si la opción de pago no existe o si el método de pago no está activo.</exception>
        /// <returns>Un objeto AddPaymentResponse que indica si el pago se ha agregado correctamente.</returns>
        private async Task<AddPaymentResponse> HandleAsync(AddPaymentCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                //busca el usuario que hizo el pago para ver si existe en la BD
                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Id == request._request.UserId);


                if (user == null)
                {
                    throw new InvalidOperationException("Registrar pago fallido: el usuario no existe");
                }

                if (user is ProviderEntity || user is AdminEntity) {
                    throw new InvalidOperationException("Registrar pago fallido: Para hacer un pago debe ser un usuario consumidor");
                }

                //busca el servicio para ver si existe en la BD
                var service = _dbContext.ServiceEntities.FirstOrDefault(s => s.Id == request._request.ServiceId);

                if (service == null)
                {
                    throw new InvalidOperationException("Registrar pago fallido: No existe el servicio");
                }

                var option = _dbContext.PaymentOptionEntities.Where(s => s.Id == request._request.PaymentOptionId && s.ServiceId == request._request.ServiceId)
                    .FirstOrDefault();

                if (option == null)
                {
                    throw new InvalidOperationException("Registrar pago fallido: No existe la opcion de pago");
                }

                if (option.Status != "Activo") {
                    throw new InvalidOperationException("Registrar pago fallido: El metodo de pago no esta activo");
                }

                //Verificaciones para que cuando sea el servicio sea de tipo telefonia, no reciba en el request un contractnumber
                //y si no es un servicio de telefonia entonces que el request no reciba un numero de telefono


                if (service.TypeService != "Telefonia")
                {
                    if (request._request.PhoneNumber == null || request._request.PhoneNumber == "")
                    {
                        // Si el servicio no requiere un número de teléfono, establece el valor de PhoneNumber en null o en una cadena vacía
                        request._request.PhoneNumber = ""; // o request._request.PhoneNumber = null;
                    }
                    else
                    {
                        throw new InvalidOperationException("Registrar pago fallido: El servicio no requiere un numero, requiere un numero de contrato nada mas");
                    }
                }

                if (service.TypeService == "Telefonia")
                {
                    if (request._request.ContractNumber == null || request._request.ContractNumber == "")
                    {
                        // Si el servicio de telefonia no tiene un número de contrato, establece el valor de ContractNumber en null o en una cadena vacía
                        request._request.ContractNumber = ""; // o request._request.ContractNumber = null;
                    }
                    else
                    {
                        throw new InvalidOperationException("Registrar pago fallido: El servicio de telefonia no tiene un numero de contrato");
                    }
                }

                    var payment = new BillEntity
                {
                    ContractNumber = request._request.ContractNumber,   
                    PhoneNumber = request._request.PhoneNumber,
                    Amount = request._request.Amount,
                    UserId = request._request.UserId,
                    ServiceId = request._request.ServiceId,
                    PaymentOptionId = request._request.PaymentOptionId,
                    Date = DateTime.Now.Date
                };

                _dbContext.BillEntities.Add(payment);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                var response = new AddPaymentResponse
                {
                    success = true,
                    message = "Pago registrado con exito"
                };
                _logger.LogInformation("AddPaymentCommandHandler.HandleAsync {Response}", response);
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
