using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para agregar un valor de prueba.
    /// </summary>
    public class AgregarValorePruebaCommandHandler : IRequestHandler<AgregarValorPruebaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarValoresQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase AgregarValorePruebaCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para agregar el valor de prueba.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public AgregarValorePruebaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarValoresQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para agregar un valor a una entidad.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AgregarValorPruebaCommand.</param>
        /// <param name="cancellationToken">Token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <returns>Un objeto Guid que indica el identificador del valor agregado.</returns>
        public async Task<Guid> Handle(AgregarValorPruebaCommand request, CancellationToken cancellationToken)
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
            catch(Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Método asincrónico que agrega un valor a una entidad.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AgregarValorPruebaCommand.</param>
        /// <returns>Un objeto Guid que indica el identificadordel valor agregado.</returns>
        private async Task<Guid> HandleAsync(AgregarValorPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}" , request);
                var entity = ValoresMapper.MapRequestEntity(request._request);
                _dbContext.Valores.Add(entity);
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
