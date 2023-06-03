using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class PaymentOptionsByServiceIdQuery : IRequest<List<PaymentOptionsByServiceIdResponse>>
    {
        public Guid ServiceId { get; set; }

        public PaymentOptionsByServiceIdQuery(Guid id)
        {
            ServiceId = id;
        }
    }
}
