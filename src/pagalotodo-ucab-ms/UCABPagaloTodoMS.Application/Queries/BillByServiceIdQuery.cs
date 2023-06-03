using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class BillByServiceIdQuery : IRequest<List<AllBillsQueryResponse>>
    {
        public Guid ServiceId { get; set; }

        public BillByServiceIdQuery(Guid id)
        {
            ServiceId = id;
        }
    }
}