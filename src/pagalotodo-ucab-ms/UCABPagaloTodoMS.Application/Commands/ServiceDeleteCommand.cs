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
    public class ServiceDeleteCommand : IRequest<ServiceDeleteResponse>
    {
        public ServiceDeleteRequest _request { get; set; }

        public ServiceDeleteCommand(ServiceDeleteRequest request)
        {
            _request = request;
        }
    }
}
