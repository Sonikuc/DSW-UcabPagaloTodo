using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class ChangePasswordCommand: IRequest<RecoveryPasswordResponse>
    {   
        public ChangePasswordRequest? Password { get; set; }
        public ChangePasswordCommand (ChangePasswordRequest request)
        {
            Password = request;
        }
    }
}
