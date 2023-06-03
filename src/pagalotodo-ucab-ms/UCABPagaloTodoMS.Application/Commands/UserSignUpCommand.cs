using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class UserSignUpCommand : IRequest<Guid>
    {
        public UserSignUpRequest _request { get; set; }

        public UserSignUpCommand(UserSignUpRequest request)
        {
            _request = request;
        }
    }
}
