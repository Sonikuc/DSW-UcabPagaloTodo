using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    //Esta clase representa el comando que se utilizará para enviar el correo electrónico de verificación de contraseña.

    public class SendVerificationCodeCommand : IRequest<SendPasswordResponse>
    {
        public string? Email { get; set; }
        public string? VerificationCode { get; set; }
    }
}

        