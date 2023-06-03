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
    public class VerifyRecoveryCodeQuery : IRequest<RecoveryPasswordResponse>
    {
        public InsertVerificationCodeRequest Code { get; set; }

        public VerifyRecoveryCodeQuery(InsertVerificationCodeRequest request)
        {
            Code = request;
        }
    }
}