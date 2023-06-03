using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Infrastructure.Utils;
using FluentValidation;
using UCABPagaloTodoMS.Application.Queries;

namespace UCABPagaloTodoMS.Application.Validators
{
	public class LoginValidator:AbstractValidator<UserLoginQuery>
	{
		public LoginValidator()
		{
            RuleFor(c => c.User.UserName)
               .NotEmpty().WithMessage("The UserName is required.");
            RuleFor(c => c.User.Password)
               .NotEmpty().WithMessage("The Password is required.");
        }
	}
}
