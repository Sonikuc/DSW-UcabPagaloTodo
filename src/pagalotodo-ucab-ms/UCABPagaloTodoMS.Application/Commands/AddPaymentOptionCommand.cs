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
    public class AddPaymentOptionCommand : IRequest<AddPaymentOptionResponse>
    {
        public AddPaymentOptionRequest _request { get; set; }
        public AddPaymentOptionCommand(AddPaymentOptionRequest request)
        {
            _request = request;
        }
    }
}
