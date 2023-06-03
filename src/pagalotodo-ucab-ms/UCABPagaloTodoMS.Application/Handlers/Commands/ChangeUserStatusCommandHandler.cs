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
    /// Manejador de comando para cambiar el estado de un usuario.
    /// </summary>
    public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand, ChangeUserStatusResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ChangeUserStatusCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase ChangeUserStatusCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para cambiar el estado del usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public ChangeUserStatusCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ChangeUserStatusCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para cambiar el estado de un usuario.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo ChangeUserStatusCommand.</param>
        /// <param name="cancellationToken">El token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <returns>Una tarea que devuelve un objeto ChangeUserStatusResponse con el estado del usuario modificado.</returns>
        public async Task<ChangeUserStatusResponse> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ChangeUserStatusCommandHandler.Handle: Request nulo.");
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
        /// Método asincrónico que cambia el estado de un usuario.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo ChangeUserStatusCommand.</param>
        /// <returns>Una tarea que devuelve un objeto ChangeUserStatusResponse con el estado del usuario modificado.</returns>
        private async Task<ChangeUserStatusResponse> HandleAsync(ChangeUserStatusCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                //Busca el usuario en la base de datos
                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Username == request._request.Username);


                if (user == null)
                {
                    throw new InvalidOperationException("Update fallido: el usuario no existe");
                }


                user.Status = request._request.Status; //cambia es status del usuario para guardarlo en la base de datos
                var response = new ChangeUserStatusResponse();
                response.User = user.Username;                              // crea el response que devuelve el usuario con un mensaje diciendo a cual estado cambio
                response.Message = "Se ha cambiado el estado del usuario";
                response.Status = request._request.Status;
                _dbContext.UserEntities.Update(user);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", response);
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
