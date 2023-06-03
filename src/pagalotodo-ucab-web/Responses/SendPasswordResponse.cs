using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class SendPasswordResponse
    {
        public bool? Send { get; set; }
        public string? Password { get; set; }
        public string? Message { get; set; }

    }
}
