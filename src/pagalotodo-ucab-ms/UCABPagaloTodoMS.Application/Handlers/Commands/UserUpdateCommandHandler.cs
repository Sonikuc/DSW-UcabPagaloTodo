using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para actualizar la información de un usuario existente.
    /// </summary>
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand,Guid>
	{
		private readonly IUCABPagaloTodoDbContext _dbContext;
		private readonly ILogger<UserUpdateCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase UserUpdateCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para actualizar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public UserUpdateCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UserUpdateCommandHandler> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}


        /// <summary>
        /// Manejador de comando para actualizar la información de un usuario existente.
        /// </summary>
        /// <param name="request">El comando UserUpdateCommand que contiene la información actualizada del usuario.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>El ID del usuario actualizado.</returns>
        public async Task<Guid> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la actualización de la información del usuario.
        /// </summary>
        /// <param name="request">El comando UserUpdateCommand que contiene la información actualizada del usuario.</param>
        /// <returns>El ID del usuario actualizado.</returns>
        private async Task<Guid> HandleAsync(UserUpdateCommand request)
		{
			var transaccion = _dbContext.BeginTransaction();
			try
			{
				_logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
				var user = _dbContext.UserEntities.FirstOrDefault(c => c.Username == request._request.Username);


                if (user == null)
				{
					throw new InvalidOperationException("Update fallido: el usuario no existe");
                }

				var properties = request._request.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var value = property.GetValue(request._request);
                    if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    {
                        var userProperty = user.GetType().GetProperty(property.Name);
                        if (userProperty != null && userProperty.CanWrite)
                        {
                            userProperty.SetValue(user, value);
                        }
                    }
                }


                _dbContext.UserEntities.Update(user);
				var id = user.Id;
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
