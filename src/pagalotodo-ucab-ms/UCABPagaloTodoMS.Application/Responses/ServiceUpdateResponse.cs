using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class ServiceUpdateResponse
    {
        public string? ServiceName { get; set; }
        public string? ContactNumber { get; set; }
        public string? TyperService { get; set; }
        public string? ProviderUsername { get; set; }
        public string? CompanyName { get; set; }
    }
}
