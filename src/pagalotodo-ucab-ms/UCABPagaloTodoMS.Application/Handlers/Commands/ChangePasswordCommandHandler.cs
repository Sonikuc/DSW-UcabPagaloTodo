using MassTransit.Mediator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para cambiar la contraseña de un usuario.
    /// </summary>
    public class ChangePasswordCommandHandler: IRequestHandler<ChangePasswordCommand,RecoveryPasswordResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ChangePasswordCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase ChangePasswordCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para cambiar la contraseña del usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public ChangePasswordCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ChangePasswordCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para cambiar la contraseña de un usuario.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo ChangePasswordCommand.</param>
        /// <param name="cancellationToken">El token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando o los campos de contraseña son nulos.</exception>
        /// <returns>Una tarea que devuelve un objeto RecoveryPasswordResponse que indica si se ha cambiado la contraseña correctamente.</returns>
        public Task<RecoveryPasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Password == null)
                {
                    _logger.LogWarning("ChangePasswordCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else if (string.IsNullOrEmpty(request.Password.Password))
                {
                    _logger.LogWarning("ChangePasswordCommandHandler.Handle: Campo contraseña nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else if (string.IsNullOrEmpty(request.Password.VerifyPassword))
                {
                    _logger.LogWarning("ChangePasswordCommandHandler.Handle: Campo verificar contraseña nulo.");
                    throw new ArgumentNullException(nameof(request));
                }else
                {
                    return HandleAsync(request);
                }
            }
            catch 
            {
                _logger.LogWarning("ChangePasswordCommandHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método asincrónico que cambia la contraseña de un usuario.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo ChangePasswordCommand.</param>
        /// <returns>Una tarea que devuelve un objeto RecoveryPasswordResponse que indica si se ha cambiado la contraseña correctamente.</returns>
        private async Task<RecoveryPasswordResponse> HandleAsync(ChangePasswordCommand request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("ChangePasswordCommandHandler.HandleAsync");

                if (request.Password == null)// verifica que la respuesta no se a null
                {
                    throw new InvalidOperationException("Update fallida: campos null");
                }
                else if (request.Password.Password != request.Password.VerifyPassword)// verifica que los campos de la nueva clave coincidan entre si
                {
                    
                    throw new InvalidOperationException("Update fallida: Las contraseñas no son iguales");
                }
                else
                {
                    var user = _dbContext.UserEntities.FirstOrDefault(c => c.Email == request.Password.Email);// busca el usuario donde se cambiara la clave
                    if (user == null)
                   {
                        throw new InvalidOperationException("Update fallido: el usuario no existe");
                   }
                    var savenewpassword = user.GetType().GetProperty("Password"); // obtine el campo que se modificara
                    if (savenewpassword != null && savenewpassword.CanWrite)// se revisa que no sea null y que se pueda sobreescribir
                    {
                        savenewpassword.SetValue(user, request.Password.Password);// se cambia por la nueva clave
                    }

                    _dbContext.UserEntities.Update(user); // se actualiza en bd
                    var id = user.Id;
                    await _dbContext.SaveEfContextChanges("SYSTEM");// indica quien hizo los cambios
                    transaction.Commit();// guarda los cambios hechos en bd
                    _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", id);
                    return new RecoveryPasswordResponse// retorna la respuesta y estado de la operacion
                    {
                        Message="Contrasña reestablecida con exito",
                        Veryfy = true
                    };

                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error ChangePasswordCommandHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

         
    }
}
