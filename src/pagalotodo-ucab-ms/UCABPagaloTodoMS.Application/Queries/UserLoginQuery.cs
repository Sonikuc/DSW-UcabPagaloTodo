using MediatR;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class UserLoginQuery : IRequest<UserLoginResponse>
    {
        public UserLoginRequest User { get; set; }

        public UserLoginQuery(UserLoginRequest request)
        {
            User = request;
        }

    }
}
