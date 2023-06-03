using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
	public class UserUpdateRequest
	{
		public string? Name { get; set; }
		public string? Lastname { get; set; }
		public string? Username { get; set; } 
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
