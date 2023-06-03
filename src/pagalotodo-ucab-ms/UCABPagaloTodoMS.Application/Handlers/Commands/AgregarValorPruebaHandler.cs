using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
//Esta clase la elimino del main el profesor
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
   public class AgregarValorPruebaHandler : IRequestHandler<AgregarValorPruebaCommand, Guid>

    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarValorPruebaHandler> _logger;

        public AgregarValorPruebaHandler(IUCABPagaloTodoDbContext dbContext,ILogger<AgregarValorPruebaHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<Guid> Handle(AgregarValorPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("AgregarValorPruebaHandler.Handle: Request nulo..");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return Task.FromResult(Guid.NewGuid());
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("AgregarValorPruebaHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private Task<Guid> HandleAsync(AgregarValorPruebaCommand request)
        {
            using var transaction = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorPruebaHandler.HandleAsync", request);
                /*await ValidateRules(request);
                var taxpayer = TaxPayerMapper.MapperRequestToEntity(request.Object);
                _context.Billings.Add(taxpayer);
                var billingId = taxpayer.Id;
                await _context.SaveEfContextChanges(_appSettings?.Value?.ApiUserName ?? string.Empty);
                transaction.Commit();
                return billingId;*/
                return Task.FromResult(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarValorPruebaHandler.HandleAsync. {Mensaje}", ex.Message);
                transaction?.Rollback();
                throw;
            }
        }
    }
}
