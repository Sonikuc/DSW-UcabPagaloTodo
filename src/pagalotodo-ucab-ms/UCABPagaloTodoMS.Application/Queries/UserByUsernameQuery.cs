using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class UserByUsernameQuery : IRequest<OneUserResponse>
    {
        public string Username { get; set; }

        public UserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
