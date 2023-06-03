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
    public class ServiceUpdateCommand : IRequest<ServiceUpdateResponse>
    {
        public ServiceUpdateRequest _request { get; set; }

        public ServiceUpdateCommand(ServiceUpdateRequest request)
        {
            _request = request;
        }
    }
}
