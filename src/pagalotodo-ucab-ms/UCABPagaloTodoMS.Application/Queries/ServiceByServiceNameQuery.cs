using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Queries
{
	public class ServiceByServiceNameQuery : IRequest<OneServiceResponse>
	{
		public OneServiceRequest _request { get; set; }

		public ServiceByServiceNameQuery(OneServiceRequest service)
		{
			_request = service;
		}
	}
}
