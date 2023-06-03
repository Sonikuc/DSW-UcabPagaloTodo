using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using MediatR;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class CustomerSignupCommand: IRequest<CustomerResponse>
    {
        public CustomerSignupRequest _User { get; set; }

        public CustomerSignupCommand(CustomerSignupRequest user)
        {
            _User = user;
        }
    }
}
