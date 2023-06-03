using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class ChangeUserStatusCommand : IRequest<ChangeUserStatusResponse>
    {
        public ChangeUserStatusRequest _request { get; set; }

        public ChangeUserStatusCommand(ChangeUserStatusRequest request)
        {
            _request = request;
        }
    }

}
