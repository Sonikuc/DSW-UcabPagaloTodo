using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class ProviderSignUpCommand : IRequest<Guid>
    {
        public ProviderSignUpRequest _request { get; set; }

        public ProviderSignUpCommand(ProviderSignUpRequest request)
        {
            _request = request;
        }
    }
}
