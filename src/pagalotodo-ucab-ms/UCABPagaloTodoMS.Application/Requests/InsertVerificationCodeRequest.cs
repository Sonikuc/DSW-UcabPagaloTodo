using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class InsertVerificationCodeRequest
    {
        public string? VerificationCode { get; set; }
        public string? Email { get; set; }
    }
}
