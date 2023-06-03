using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para registrar un nuevo usuario.
    /// </summary>
    public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand,Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UserSignUpCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase UserSignUpCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para registrar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public UserSignUpCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UserSignUpCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para registrar un nuevo usuario.
        /// </summary>
        /// <param name="request">El comando UserSignUpCommand que contiene la información del usuario a registrar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>El ID del usuario registrado.</returns>
        public async Task<Guid> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
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
        /// Método privado que maneja el registro del usuario.
        /// </summary>
        /// <param name="request">El comando UserSignUpCommand que contiene la información del usuario a registrar.</param>
        /// <returns>El ID del usuario registrado.</returns>
        private async Task<Guid> HandleAsync(UserSignUpCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                var result = _dbContext.UserEntities.Count(c => c.Username == request._request.Username);

                if (result > 0) {
                    throw new InvalidOperationException("Registro fallido: el usuario ya existe");
                }

                var entity = UserMapper.MapRequestToEntity(request._request);
                _dbContext.UserEntities.Add(entity);
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

