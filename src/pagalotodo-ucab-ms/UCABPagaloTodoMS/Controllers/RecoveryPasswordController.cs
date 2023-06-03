using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Core.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecoveryPasswordController : BaseController<RecoveryPasswordController>
    {
        private readonly IMediator _mediator;
        private readonly IUCABPagaloTodoDbContext _dbContext;

        public RecoveryPasswordController(IUCABPagaloTodoDbContext dbContext, ILogger<RecoveryPasswordController> logger, IMediator mediator): base(logger)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Endpoint para la recuperación de contraseña.
        /// </summary>
        /// <remarks>
        /// Este endpoint busca un usuario en la base de datos que tenga la dirección de correo electrónico proporcionada en la solicitud.
        /// Si encuentra un usuario, genera un código de verificación de seis dígitos y envía un correo electrónico que contiene el código de
        /// verificación al usuario.
        /// </remarks>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con la propiedad "Email" que contiene la dirección de correo electrónico del usuario.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información de la verificación enviada.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <response code="404">No se encuentra un usuario con la dirección de correo electrónico proporcionada.</response>
        /// <response code="500">Se produjo un error interno en el servidor al procesar la solicitud.</response>
        /// <returns>Objeto JSON con la información de la verificación enviada.</returns>
        /// 

        [HttpPost("RecoveryPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //El método busca un usuario en la base de datos que tenga la dirección de correo electrónico proporcionada en la solicitud.
        //Si encuentra un usuario, genera un código de verificación de seis dígitos y envía un correo electrónico que contiene el código de verificación al usuario.
        public async Task<IActionResult> Post([FromBody] RecoveryPasswordRequest request)  {
            var transaccion = _dbContext.BeginTransaction();
            _logger.LogInformation("Entrando a la acción de recuperación de contraseña");
            try
            {
                
                var user = await _dbContext.UserEntities.FirstOrDefaultAsync(c => c.Email == request.Email);
                if (user == null)
                {
                    return NotFound("La dirección de correo electrónico no está registrada");
                }

                var command = new SendVerificationCodeCommand
                {
                    Email = request.Email,
                    VerificationCode = GenerateVerificationCode(), 
                };

                //guarda el codigo generado en VerificationCode en la BD
                var userCodeVerification = user.GetType().GetProperty("VerificationCode");
                if (userCodeVerification != null && userCodeVerification.CanWrite)
                {
                    userCodeVerification.SetValue(user, command.VerificationCode);
                }

                _dbContext.UserEntities.Update(user);
                await _dbContext.SaveEfContextChanges("SYSTEM");
                var response = await _mediator.Send(command);
                transaccion.Commit();
                return Ok(response);
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                _logger.LogError(ex, "Error en la acción de recuperación de contraseña");
                return StatusCode(500, "Ha ocurrido un error en el servidor");
            }
        }

        /// <summary>
        /// Genera un código de verificación de seis dígitos.
        /// </summary>
        /// <returns>Un string que representa el código de verificación generado.</returns>
        /// 

        private static string GenerateVerificationCode()
        {
            // Generar un código de verificación de 6 dígitos
            var random = new Random();
            var verificationCode = random.Next(100000, 999999).ToString();
            return verificationCode;
        }

        /// <summary>
        /// Endpoint para la verificación de código de recuperación de contraseña.
        /// </summary>
        /// <remarks>
        /// Este endpoint verifica que el código introducido por el usuario coincida con el que se almacena en la base de datos.
        /// </remarks>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con la propiedad "Email" que contiene la dirección de correo electrónico
        /// del usuario y la propiedad "Code" que contiene el código de verificación introducido por el usuario.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica si el código de verificación es válido.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <response code="404">No se encuentra un usuario con la dirección de correo electrónico proporcionada.</response>
        /// <response code="500">Se produjo un error interno en el servidor al procesar la solicitud.</response>
        /// <returns>Objeto JSON que indica si el código de verificación es válido.</returns>
        /// 

        [HttpPost("VerifyCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // En este endpoint se verifica que el codigo introducido por el usuario coincida con el que se almaceno en la base de datos
        public async Task<IActionResult> Verify([FromBody] InsertVerificationCodeRequest request)
        {
            var transaccion = _dbContext.BeginTransaction();
            _logger.LogInformation("Entrando a la acción de Verificacion del Codigo de recuperacion de contraseña");

            try
            { 
                if (request == null)
                {
                     return BadRequest("La solicitud no puede ser nula");
                }
                var user = await _dbContext.UserEntities.SingleOrDefaultAsync(c => c.Email == request.Email);

                if (user == null)
                {

                    return NotFound("La dirección de correo electrónico no está registrada");
                }
               
                else
                {
                    var query = new VerifyRecoveryCodeQuery(request);
                    var result = await _mediator.Send(query);
                    return Ok(result);
                }
               
            }
            catch (ArgumentNullException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la acción de Verificar el codigo");
                return StatusCode(500, "Ha ocurrido un error en el servidor");
            }
            
        }
        /// <summary>
        /// Endpoint para el cambio de contraseña.
        /// </summary>
        /// <remarks>
        /// Una vez validado el código de verificación, se cambia la clave de la cuenta.
        /// </remarks>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con la propiedad "Email" que contiene la dirección de correo electrónico del usuario,
        /// la propiedad "Code" que contiene el código de verificación introducido por el usuario y la propiedad "NewPassword" que contiene la nueva contraseña del usuario.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que indica que la contraseña se ha cambiado correctamente.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <response code="500">Se produjo un error interno en el servidor al procesar la solicitud.</response>
        /// <returns>Objeto JSON que indica que la contraseña se ha cambiado correctamente.</returns>
        /// 

        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // En este Endpoint, una vez validado el codigo, se cambia la clave de la cuenta 
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var transaccion = _dbContext.BeginTransaction();
            _logger.LogInformation("Entrando a la acción de Cambio de contraseña");
           
            try
            {
                var command = new ChangePasswordCommand(request);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Campos nulos o vacios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la acción de cambiar la constraseña");
                return StatusCode(500, "Ha ocurrido un error en el servidor");
            }
        }
    }

    
}
