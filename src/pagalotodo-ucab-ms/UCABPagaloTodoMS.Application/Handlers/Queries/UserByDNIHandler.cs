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
    /// Manejador de la consulta que busca usuarios por su número de identificación personal (DNI).
    /// </summary>
    public class UserByDNIHandler : IRequestHandler<UserByDNIQuery, List<OneUserResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UserByDNIHandler> _logger;


        /// <summary>
        /// Constructor de la clase UserByDNIHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar los usuarios.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public UserByDNIHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UserByDNIHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca usuarios por su número de identificación personal (DNI).
        /// </summary>
        /// <param name="request">La consulta UserByDNIQuery que especifica el DNI de los usuarios que se buscan.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos OneUserResponse que contienen información detallada de los usuarios encontrados.</returns>
        public Task<List<OneUserResponse>> Handle(UserByDNIQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de usuarios por su número de identificación personal (DNI).
        /// </summary>
        /// <param name="request">La consulta UserByDNIQuery que especifica el DNI de los usuarios que se buscan.</param>
        /// <returns>Una lista de objetos OneUserResponse que contienen información detallada de los usuarios encontrados.</returns>
        private async Task<List<OneUserResponse>> HandleAsync(UserByDNIQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = _dbContext.UserEntities.Where(c => c.Dni == request.Dni).Select(c => new OneUserResponse()
                {
                    Dni = c.Dni,
                    Username = c.Username,
                    Name = c.Name,
                    Lastname = c.Lastname,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Status = c.Status,

                });

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
