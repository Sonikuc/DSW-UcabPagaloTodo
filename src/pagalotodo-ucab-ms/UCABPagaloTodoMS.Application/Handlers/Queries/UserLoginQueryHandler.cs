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
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de la consulta que busca un usuario por su nombre de usuario y contraseña.
    /// </summary>
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, UserLoginResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UserLoginQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase UserLoginQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el usuario.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public UserLoginQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UserLoginQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca un usuario por su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="request">La consulta UserLoginQuery que especifica el usuario y contraseña del usuario que se busca.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto UserLoginResponse que contiene información detallada del usuario encontrado.</returns>
        public Task<UserLoginResponse> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request==null )
                {
    
                    _logger.LogWarning("UserLoginQueryHandler.Handle: Request null.");               
                    throw new ArgumentNullException(nameof(request), "La solicitud no puede ser nula");// lee campo nulo

                }
                else if(string.IsNullOrEmpty(request.User.UserName))
                {

                    _logger.LogWarning("UserLoginQueryHandler.Handle: Campo UserName esta vacio.");
                    throw new ArgumentException(nameof(request), "El campo de User Name no puede estar vacío.");//lee campos vacios
                    
                }else if (string.IsNullOrEmpty(request.User.Password))
                {
                    _logger.LogWarning("UserLoginQueryHandler.Handle: Campo UserName esta vacio.");
                    throw new ArgumentException("El campo de nombre de password no puede estar vacío.", nameof(request));
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
        /// Método asincronico que maneja la búsqueda de un usuario por su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="request">La consulta UserLoginQuery que especifica el usuario y contraseña del usuario que se busca.</param>
        /// <returns>Un objeto UserLoginResponse que contiene información detallada del usuario encontrado.</returns>
        public async Task<UserLoginResponse> HandleAsync(UserLoginQuery request)
        {
            var transaction = _dbContext.BeginTransaction();

            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var userExists = _dbContext.UserEntities.Any(c => c.Username == request.User.UserName);

                if (userExists)
                {
                    try
                    {
                        var result = await _dbContext.UserEntities.Where(c => c.Username == request.User.UserName && c.Password == request.User.Password).FirstOrDefaultAsync();
             
                        if (result != null)
                        {
                            if (result is AdminEntity admin)
                            {
                                var adminresponse = new UserLoginResponse
                                {
                                    Id = result.Id,
                                    UserName = result.Username,
                                    Success = true,
                                    Message = "Inicio de sesión exitoso",
                                    isAdmin = true,
                                    isProvider = false,
                                    Status = true
                                };
                                return adminresponse;
                            }
                            if (result is ProviderEntity provider)
                            {
                                var providerresponse = new UserLoginResponse
                                {
                                    Id = result.Id,
                                    UserName = result.Username,
                                    Success = true,
                                    Message = "Inicio de sesión exitoso",
                                    isAdmin = false,
                                    isProvider = true,
                                    Status = result.Status
                                };
                                return providerresponse;
                            }
                            var response = new UserLoginResponse
                            {
                                Id = result.Id,
                                UserName = result.Username,
                                Success = true,
                                Message = "Inicio de sesión exitoso",
                                isAdmin = false,
                                isProvider = false,
                                Status = result.Status
                            };
                            return response;
                            // Usuario encontrado y autenticado correctamente
                        }
                        else
                        {
                            // Usuario encontrado, pero la contraseña no coincide
                            return new UserLoginResponse { Success = false, Message = "Contraseña incorrecta", UserName = "", Password = "" };
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                    }

                    return new UserLoginResponse { Success = false, Message = "Contraseña incorrecta", UserName = "", Password = "" };

                }
                else
                {
                    // Usuario no encontrado
                    return new UserLoginResponse { Success = false, Message = "Usuario no encontrado", UserName = "", Password = "" } ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }

    }
}