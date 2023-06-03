using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Interfaces;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Manejador de comando para enviar un código de verificación por correo electrónico.
    /// </summary>
    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, SendPasswordResponse>
    {
            private readonly IEmailService _emailService;

            public SendVerificationCodeCommandHandler(IEmailService emailService)
            {
                _emailService = emailService;
            }

        /// <summary>
        /// Método que maneja el comando para enviar un código de verificación por correo electrónico.
        /// </summary>
        /// <param name="request">El objeto de comando de tipo SendVerificationCodeCommand.</param>
        /// <param name="cancellationToken">El token de cancelación para cancelar la operación asincrónica.</param>
        /// <returns>Una tarea queretorna un objeto Unit de MediatR.</returns>

        public async Task<SendPasswordResponse> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
             
                var subject = "Codigo de Verificacion";
                var body = $"Tu codigo de verifiacion es: {request.VerificationCode}";
                
                if (request.Email != null)
                {
                    await  _emailService.SendEmailAsync(request.Email, subject, body);
        }
                else
                {
                    return new SendPasswordResponse { Message = "Email nulo, no es posible enviar codigo de verificacion", Send = false };
                }
               
                
                return new SendPasswordResponse { Message="Se envio a su correo un mensaje con su codigo de verificacion", Send = true};
            }
    }
} 
