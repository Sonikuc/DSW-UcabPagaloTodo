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
	public class UserUpdateCommand : IRequest<Guid>
	{
		public UserUpdateRequest _request { get; set; }

		public UserUpdateCommand(UserUpdateRequest request)
		{
			_request = request;
		}
	}
}
