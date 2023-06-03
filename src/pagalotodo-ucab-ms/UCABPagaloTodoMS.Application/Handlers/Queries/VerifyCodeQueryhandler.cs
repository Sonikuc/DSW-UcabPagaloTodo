using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de la consulta VerifyRecoveryCodeQuery que verifica la validez de un código de recuperación de contraseña.
    /// </summary>
    public class VerifyCodeQueryhandler : IRequestHandler<VerifyRecoveryCodeQuery, RecoveryPasswordResponse>
    {
        private readonly ILogger<VerifyCodeQueryhandler> _logger;
        private readonly IUCABPagaloTodoDbContext _dbContext;


        /// <summary>
        /// Constructor de la clase VerifyCodeQueryhandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el código de recuperación.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public VerifyCodeQueryhandler(IUCABPagaloTodoDbContext dbContext, ILogger<VerifyCodeQueryhandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta VerifyRecoveryCodeQuery que verifica la validez de un código de recuperación de contraseña.
        /// </summary>
        /// <param name="request">La consulta VerifyRecoveryCodeQuery que especifica el código de recuperación que se va a verificar.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto RecoveryPasswordResponse que indica si el código de recuperación es válido.</returns>
        public Task<RecoveryPasswordResponse> Handle(VerifyRecoveryCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("VerifyCodeQueryhandler.Handler: Request null");
                    throw new ArgumentNullException(nameof(request), "La solicitud es nula (NO SE HA INSERTADO EL CODIGO)");
                }
                else
                    return HandleAsync(request);
            }
            catch
            {
                _logger.LogWarning("VerifyCodeQueryhandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método asincronico que maneja la verificación de un código de recuperación de contraseña.
        /// </summary>
        /// <param name="request">La consulta VerifyRecoveryCodeQuery que especifica el código de recuperación que se va a verificar.</param>
        /// <returns>Un objeto RecoveryPasswordResponse que indica si el código de recuperación es válido.</returns>
        public async Task<RecoveryPasswordResponse> HandleAsync(VerifyRecoveryCodeQuery request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("VerifyCodeQueryhandler.HandleAsync");

                var RecoveyCode = await _dbContext.UserEntities.FirstOrDefaultAsync(c => c.VerificationCode == request.Code.VerificationCode);

                // Verificar si el código existe y si no ha expirado
                if (RecoveyCode == null || RecoveyCode.VerificationCode != request.Code.VerificationCode)
                {
                    // Si el código no es válido, se devuelve un error
                    return new RecoveryPasswordResponse
                    {
                        Veryfy = false,
                        Message = "Verificacion de codigo de recuperacion fallido"
                    };
                }
                else
                {
                    // Si el código es válido, se puede proceder con el cambio de contraseña
                    return new RecoveryPasswordResponse
                    {
                        Veryfy = true,
                        Message = "Verificacion de codigo de recuperacion exitosa"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Ocurrio un error en la consulta en la cuonsulta del VerificationCode Exception: "
                    + ex);
                throw;
            }
        }
    }
}
