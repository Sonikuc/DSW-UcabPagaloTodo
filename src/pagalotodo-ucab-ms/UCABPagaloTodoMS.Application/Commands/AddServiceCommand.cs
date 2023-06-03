using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
	public class AddServiceCommand : IRequest<Guid>
	{
		public AddServiceRequest _request { get; set; }
		public AddServiceCommand(AddServiceRequest request) 
		{
			_request = request;
		}
	}
}
