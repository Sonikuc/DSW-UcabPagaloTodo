using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class ServiceUpdateRequest
    {
        public string? ServiceName { get; set; }
        public string? TypeService { get; set; }
        public string? ContactNumber { get; set; }
        public string? UserName { get; set; }
    }
}
