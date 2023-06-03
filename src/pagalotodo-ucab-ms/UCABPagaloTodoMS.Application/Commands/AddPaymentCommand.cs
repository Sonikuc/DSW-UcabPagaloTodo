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
    public class AddPaymentCommand : IRequest<AddPaymentResponse>
    {
        public AddPaymentRequest _request { get; set; }
        public AddPaymentCommand(AddPaymentRequest request)
        {
            _request = request;
        }
    }
}
