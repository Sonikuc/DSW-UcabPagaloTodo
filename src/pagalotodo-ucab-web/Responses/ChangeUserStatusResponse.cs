using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class ChangeUserStatusResponse
    {
        public string? User { get; set; }
        public string? Message { get; set; }
        public bool? Status { get; set; }

    }
}
