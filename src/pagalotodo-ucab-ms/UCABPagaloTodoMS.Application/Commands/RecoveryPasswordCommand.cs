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
    public class RecoveryPasswordCommand : IRequest<RecoveryPasswordResponse>
    {
        public RecoveryPasswordRequest _request { get; set; }

        public RecoveryPasswordCommand (RecoveryPasswordRequest request)
        {
            _request = request;
        }
    }
    
}
