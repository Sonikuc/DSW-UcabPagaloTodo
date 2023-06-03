using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class ChangePasswordRequest
    {
        public string? Password { get; set; }
        public string? VerifyPassword { get; set; }
        public string? Email { get; set; }
    }
}
