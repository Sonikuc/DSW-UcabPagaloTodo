using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para agregar un servicio.
    /// </summary>
    public class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, Guid>
	{
		private readonly IUCABPagaloTodoDbContext _dbContext;
		private readonly ILogger<AddServiceCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase AddServiceCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para agregar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public AddServiceCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddServiceCommandHandler> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}


        /// <summary>
        /// Manejador de comando para agregar un servicio a un proveedor.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddServiceCommand.</param>
        /// <param name="cancellationToken">Token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario no existe o no es un proveedor, o si ya existe un servicio con el mismo nombre.</exception>
        /// <returns>Un objeto Guid que indica el identificador del servicio agregado.</returns>
        public async Task<Guid> Handle(AddServiceCommand request, CancellationToken cancellationToken)
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
        /// Método asincrónico que agrega un servicio a un proveedor.
        /// </summary/// <param name="request">Objeto de comando de tipo AddServiceCommand.</param>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario no existe o no es un proveedor, o si ya existe un servicio con el mismo nombre.</exception>
        /// <returns>Un objeto Guid que indica el identificador del servicio agregado.</returns>
        private async Task<Guid> HandleAsync(AddServiceCommand request)
		{
			var transaccion = _dbContext.BeginTransaction();
			try
			{
				_logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
				var user = _dbContext.UserEntities.FirstOrDefault(c => c.Username == request._request.UserName);


				if (user == null)
				{
					throw new InvalidOperationException("Registrar servicio fallido: el usuario no existe");
				}

				user.GetType();

				if (!(user is ProviderEntity provider)) {
					throw new InvalidOperationException("Registrar servicio fallido: el usuario no es un proveedor");
				}

				var consult = _dbContext.ServiceEntities.Where(p => p.ProviderId == user.Id).Count(c => c.ServiceName == request._request.ServiceName);

                if (consult >0)
				{
                    throw new InvalidOperationException("Registrar servicio fallido: Ya hay un servicio con este nombre");
                }

                var service = ServiceMapper.MapRequestToEntity(request._request);
				service.Provider = provider;
				service.ProviderId = provider.Id;

				_dbContext.ServiceEntities.Add(service);
				await _dbContext.SaveEfContextChanges("APP");
				transaccion.Commit();
				_logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", service.Id);
				return service.Id;
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
