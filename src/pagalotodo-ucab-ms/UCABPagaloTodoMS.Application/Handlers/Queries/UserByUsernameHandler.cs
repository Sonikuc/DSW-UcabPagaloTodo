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
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de la consulta que busca un usuario por su nombre de usuario.
    /// </summary>
    public class UserByUsernameHandler : IRequestHandler<UserByUsernameQuery, OneUserResponse>
    {

        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UserByUsernameHandler> _logger;


        /// <summary>
        /// Constructor de la clase UserByUsernameHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public UserByUsernameHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UserByUsernameHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="request">La consulta UserByUsernameQuery que especifica el nombre de usuario del usuario que se busca.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto OneUserResponse que contiene información detallada del usuario encontrado.</returns>
        public Task<OneUserResponse> Handle(UserByUsernameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UserLoginQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }


            }
            catch
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método privado que maneja la búsqueda de un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="request">La consulta UserByUsernameQuery que especifica el nombre de usuario del usuario que se busca.</param>
        /// <returns>Un objeto OneUserResponse que contiene información detallada del usuario encontrado.</returns>
        private async Task<OneUserResponse> HandleAsync(UserByUsernameQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = await _dbContext.UserEntities.Where(c => c.Username == request.Username).FirstOrDefaultAsync();


                if (result == null)
                {
                    throw new InvalidOperationException("Consultar por username fallido: No existe el usuario");
                }

                var response = new OneUserResponse
                {
                    Dni = result.Dni,
                    Username = result.Username,
                    Name = result.Name,
                    Lastname = result.Lastname,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber,
                    Status = result.Status
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
