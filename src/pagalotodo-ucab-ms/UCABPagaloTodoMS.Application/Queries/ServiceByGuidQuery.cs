using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ServiceByGuidQuery : IRequest<OneServiceResponse>
    {
        public Guid ServiceId { get; set; }

        public ServiceByGuidQuery(Guid id) 
        { 
            ServiceId = id;
        } 
    }
}
