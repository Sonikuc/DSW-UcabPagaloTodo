using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class BillByUserIdQuery : IRequest<List<AllBillsQueryResponse>>
    {
        public Guid UserId { get; set; }

        public BillByUserIdQuery(Guid id)
        {
            UserId = id;
        }
    }
}
